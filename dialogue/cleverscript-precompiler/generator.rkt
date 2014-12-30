#lang racket

(provide sentence
         phrase
         combine)

;; Define sentence -- it is an ordered list of text and phrases.
;; A sentence is whatever exists between two '/' separation characters in the
;; CleverScript file.
(struct sentence (lst) #:transparent)

;; Phrases contain a symbol (label) that corresponds to a sentence in 
;; phrase-ht.
(struct phrase (lst) #:transparent) 

;; Produces a list of strings representing the language model of a sentence.
(define (combine my-sentence)
  (define (combine-string-list lst)
    (define (combine-iter accum remaining)
      (cond [(empty? remaining) accum]
            [else (combine-iter 
                    (string-append accum (car remaining))
                    (cdr remaining))]))
    (combine-iter "" lst))
  ;; Extracts the (combined) sentences from a phrase as a list of strings.
  (define (extract-sentences phr)
    (define (extract-iter accum remaining)
      (define (all-strings? lst)
        (andmap (λ (x) (string? x)) lst))
      (cond
        [(empty? remaining) accum]
        [(all-strings? (sentence-lst (car remaining))) 
         (extract-iter 
           (cons (combine-string-list (sentence-lst (car remaining)))
                 accum)
           (cdr remaining))]
        [else (extract-iter
                (append 
                  accum 
                  (combine (car remaining)))
                (cdr remaining))]))
    (extract-iter empty (phrase-lst phr)))
  ;; For lists of N and M strings, returns N*M strings each of M appended to
  ;; all of the original N strings.
  (define (append-phrase orig-strs new-strs)
    (define (append-phrase-iter orig-strs accum remaining)
      (cond
        [(empty? remaining) accum]
        [else 
          (define new-accum 
            (append accum 
                    (map (λ (str) (string-append str (car remaining))) 
                         orig-strs)))
          (append-phrase-iter orig-strs new-accum (cdr remaining))]))
    (append-phrase-iter orig-strs empty new-strs))
  (define (combine-iter accum remaining)
    (cond 
      [(empty? remaining) accum]
      [(string? (car remaining)) 
       (begin
        (define new-accum 
          (map (λ (str) (string-append str (car remaining))) 
               accum))
        (combine-iter new-accum (cdr remaining)))]
      [(phrase? (car remaining))
       (begin
         (define phrase-strings (extract-sentences (car remaining))) 
         (define new-accum (append-phrase accum phrase-strings)) 
         (combine-iter new-accum (cdr remaining)))]
      [else (error "Element besides string or phrase in sentence.")]))
  (combine-iter (list "") (sentence-lst my-sentence)))
