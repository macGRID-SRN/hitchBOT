#lang racket

(require "parser.rkt"
         "generator.rkt")

;; NOTE(brendan): takes an input value -- max number of sentences to print
;; Print that max number of sentences from each list of sentences (or the
;; entire list if its length is less than the max number)
(define (print-sentences lst filename max-per-input) 
  (define (lst-to-string lst max-per-input)
    (define take-num (min (length lst) max-per-input))
    (define after-last (cond [(> (length lst) 0) "\n"]
                             [else ""]))
    (string-join
      (map (λ (str) (string-normalize-spaces str))
           (take (shuffle lst) take-num))
      "\n"
      #:after-last after-last))

  (define (print-sentence s)
    (call-with-output-file 
      filename 
      #:exists 'append 
      (λ (out) (display (lst-to-string (combine s) max-per-input) out))))
  (begin
    (when (file-exists? filename)
      (delete-file filename))
    (for-each (λ (s) (print-sentence s))
             lst)))

(define (make-language-model input-file output-file max-per-input)
  (define my-sentences (make-sentence-lst input-file))
  (print-sentences my-sentences output-file max-per-input))

;; TODO(brendan): Put these in a function for convenience of defining new
;; command-line variables.
(define input-file
  (cond
    [(< (vector-length (current-command-line-arguments)) 1)
     (error "No input-file command-line argument")]
    [else (vector-ref (current-command-line-arguments) 0)]))

(define output-file
  (cond
    [(< (vector-length (current-command-line-arguments)) 2)
     (error "No output-file command-line argument")]
    [else (vector-ref (current-command-line-arguments) 1)]))

(define max-per-input
  (cond
    [(< (vector-length (current-command-line-arguments)) 3)
     (error "No max. sentences per input command-line argument")]
    [else (string->number (vector-ref (current-command-line-arguments) 2))]))

(make-language-model input-file output-file max-per-input)
