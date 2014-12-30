#lang racket

(require "parser.rkt"
         "generator.rkt")

(define (print-sentences lst filename) 
  (define (lst-to-string lst)
    (string-join 
      (map (λ (str) (string-normalize-spaces str))
           lst) 
      "\n" 
      #:after-last "\n"))
  (define (print-sentence s)
    (call-with-output-file 
      filename 
      #:exists 'append 
      (λ (out) (display (lst-to-string (combine s)) out))))
  (begin
    (when (file-exists? filename)
      (delete-file filename))
    (for-each (λ (s) (print-sentence s))
             lst)))


(define (make-language-model input-file output-file)
  (define my-sentences (make-sentence-lst input-file))
  (print-sentences my-sentences output-file))

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

(make-language-model input-file output-file)
