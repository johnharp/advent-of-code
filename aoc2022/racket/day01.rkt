#lang racket
(require advent-of-code)


(define puzzle-input (fetch-aoc-input (find-session) 2022 1 #:cache #t))

(define (sum-list lst) (apply + lst))
(define (max-list lst) (apply max lst))

(define groups (string-split puzzle-input "\n\n"))

(define (sum-group group)
  (sum-list
   (map string->number
       (string-split group "\n"))))

(define group-sums (map sum-group groups))

; Part 1 Answer
(max-list group-sums)

; Part 2 Answer
(sum-list (take (sort group-sums >) 3))



