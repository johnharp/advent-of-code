#lang racket

(require "move.rkt")

(provide filter-no-op
         filter-blocked)

(define (filter-no-op state moves)
  moves)

(define (filter-blocked state moves)
  (filter  (lambda (m) (not (is-move-blocked state m))) moves))

