#lang racket

(require "generator.rkt")

(provide make-sentence-lst)

;; Interleave lst1 and lst2 with lst1[0] starting first (if lst1 != null).
(define (zip lst1 lst2)
  (cond
    [(empty? lst1) lst2]
    [(empty? lst2) lst1]
    [else (append (list (car lst1) (car lst2))
                  (zip (cdr lst1) (cdr lst2)))]))

;; Make a list of all the phrase symbols in the line.
(define (line-to-phrases str)
  (define (remove-brackets str)
    (regexp-replace #rx"\\(\\((.+?)\\)\\)" str "\\1"))
  (map string->symbol 
       (map remove-brackets 
            (map string-downcase 
                 (regexp-match* #rx"\\(\\(.+?\\)\\)" str)))))

;; Make a list of all the non-phrase strings in the line.
(define (line-to-texts str)
  (regexp-split #rx"\\(\\(.+?\\)\\)" str))

;; Replace first occurrences of opt-rx in each string in list and add strings
;; with those occurrences removed to the list.
(define (trans-opt-lst opt-rx replace-str lst)
  (define (translate-optional str)
    (cond
      [(regexp-match? opt-rx str) 
       (cons (regexp-replace opt-rx str replace-str) 
             (regexp-replace opt-rx str ""))]
      [else str]))
  (flatten (map (λ (str) (translate-optional str)) lst)))

;; Take a list of strings and recursively translate optional terms.
(define (trans-opt-recurs lst)
  (define opt-rx1 #rx"\\(\\(!\\?(.+?)\\)\\)")
  (define opt-rx2 #rx"\\(\\(\\?(.+?)\\)\\)")
  (define opt-rx3 #rx"\\(!\\?(.+?)\\)")
  (define opt-rx4 #rx"\\(\\?(.+?)\\)")
  (define replace-phr "((\\1))")
  (define replace-word "\\1")
  (define (optionals-left? opt-rx lst) 
    (ormap (λ (str) (regexp-match? opt-rx str)) lst))
  (cond [(optionals-left? opt-rx1 lst)
         (trans-opt-recurs (trans-opt-lst opt-rx1 replace-phr lst))]
        [(optionals-left? opt-rx2 lst)
         (trans-opt-recurs (trans-opt-lst opt-rx2 replace-phr lst))]
        [(optionals-left? opt-rx3 lst)
         (trans-opt-recurs (trans-opt-lst opt-rx3 replace-word lst))]
        [(optionals-left? opt-rx4 lst)
         (trans-opt-recurs (trans-opt-lst opt-rx4 replace-word lst))]
        [else lst]))

;; Get label-(list of phrases/text) pair from phrase string and label.
(define (get-raw-fragment str)
  (define between-tabs (string-split str "\t"))
  (define phrase-str (list-ref between-tabs 3))
  (define sentence-frags (string-split phrase-str "/"))
  (define frags-normalized (map (λ (str) (string-normalize-spaces str)) 
                                sentence-frags))
  (define translated-optionals (trans-opt-recurs sentence-frags))
  (define text-lsts (map line-to-texts translated-optionals))
  (define phrases-lsts (map line-to-phrases translated-optionals))
  (define label (string->symbol 
                  (string-downcase (list-ref between-tabs 1))))
  (cons label (map zip text-lsts phrases-lsts)))

;; Map a value (list of lists of strings or string-only phrases) to a phrase.
(define (value->phrase value)
  (define (translate-list lst)
    (define (lst-o-lsts->phrase list-of-lists)
      (phrase (map sentence list-of-lists)))
    (define (selective-phrase elem)
      (cond 
        [(list? elem) (lst-o-lsts->phrase elem)]
        [else elem]))
    (cond [(andmap string? lst) (sentence lst)]
          [else (sentence (map selective-phrase lst))]))
  (phrase (map translate-list value)))

;; Check if str starts with rx-str
(define (starts-with? rx-str str)
  (regexp-match? (regexp (string-append "^" rx-str)) str))

;; Parses file "filename" and returns all phrases in hash-table phrase-ht.
(define (make-phrase-ht filename)
  ;; NOTE(brendan): remove line-numbers and do checking for phrase existence
  ;; here.
  ;; Map the labels in a phrase hash-table to a phrase structure (string list).
  (define (translate-phrases ht)
    ;; Check if string and not all whitespace.
    (define (visible-string? maybe-str)
      (and (string? maybe-str)
           (not (regexp-match? #rx"^ *$" maybe-str))))
    ;; Check if list contains all white-space strings.
    (define (not-whitespace-list? lst) 
      (not (andmap visible-string? lst)))
    (define (phrase->strings symb parent-key)
      ; NOTE(brendan): print error message if phrase doesn't exist and
      ; return null
      (define phrase-lst
        (cond
          [(hash-has-key? ht symb) (cdr (hash-ref ht symb))]
          [else (begin
                  (displayln 
                    (string-append "Error, no such phrase: " 
                                   (symbol->string symb)
                                   " (line number: "
                                   (number->string 
                                     (car (hash-ref ht parent-key)))
                                   ")"))
                  null)]))
        (map (λ (x) (filter string? x)) 
             (filter not-whitespace-list? phrase-lst)))
    (define (picky-phrase->strings maybe-str parent-key)
      (cond 
        [(symbol? maybe-str) (phrase->strings maybe-str parent-key)]
        [else maybe-str]))
  ;; Cons a key with a phrase filtered to be a list of strings
  (define (make-key-value key value)
    (define (single-arg-picky-phrase->string lst)
      (picky-phrase->strings lst key))
    (cons key
          (value->phrase
            (map (λ (lst) (map single-arg-picky-phrase->string lst)) 
                 (cdr value)))))
    (make-immutable-hash
      (hash-map ht make-key-value)))

  ; NOTE(brendan): temporarily put line numbers as the first element of the
  ; phrase-lst here, for error-checking during translate-phrases.
  (define (make-raw-ht-iter ht in line-number)
    (begin
      (define next-line (read-line in))
      (cond
        [(eof-object? next-line) ht]
        [(starts-with? "phrase" next-line) 
         (define raw-phrase (get-raw-fragment next-line)) 
         (make-raw-ht-iter
           (hash-set ht (car raw-phrase) (cons line-number (cdr raw-phrase))) 
           in (+ line-number 1))]
        [else (make-raw-ht-iter ht in (+ line-number 1))])))
  (begin
    (define in (open-input-file filename))
    (define phrase-ht (make-raw-ht-iter #hash() in 1))
    (close-input-port in)
    (translate-phrases phrase-ht)))

;; Read in inputs into a list of sentences.
(define (make-sentence-lst filename)
  (define phrase-ht (make-phrase-ht filename))
  ;; Map any symbols in a list to its phrase in the phrase hash-table.
  (define (translate-phrases lst line-number)
    (define (picky-symb->phrase maybe-symbol)
      (cond 
        [(symbol? maybe-symbol) 
         ; TODO(brendan): stick this in a function
         ; NOTE(brendan): returns sentence corresponding to phrase, or null
         ; if symbol is not in phrase-ht
         (cond 
           [(hash-has-key? phrase-ht maybe-symbol) 
            (hash-ref phrase-ht maybe-symbol)]
           [else 
             (begin 
               (displayln (string-append 
                            "Error, no such phrase: "
                            (symbol->string maybe-symbol)
                            " (line number: "
                            (number->string line-number)
                            ")"))
               (phrase null))])]
        [else maybe-symbol]))
    (map picky-symb->phrase lst))
  ; TOOD(brendan): move line-number to sentence struct?
  (define (make-lst-iter lst in line-number)
    (define next-line (read-line in))
    (cond
      [(eof-object? next-line) lst]
      [(starts-with? "input" next-line)
       (define raw-sentence 
         (cdr (get-raw-fragment next-line)))
       (define new-lst 
         (append 
           (map sentence 
                ; NOTE(brendan): pass line-number to translate-phrases with 
                ; lambda function
                (map (λ (s) (translate-phrases s line-number)) raw-sentence)) 
           lst)) 
       (make-lst-iter new-lst in (+ line-number 1))]
      [else (make-lst-iter lst in (+ line-number 1))]))
  (begin
    (define in (open-input-file filename))
    (define sentence-lst (make-lst-iter empty in 1))
    (close-input-port in)
    sentence-lst))
