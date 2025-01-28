#lang racket
(require advent-of-code)

(define rock 0)
(define paper 1)
(define scissors 2)

(define (mine round) (second round))
(define (theirs round) (first round))

(define puzzle-input (fetch-aoc-input (find-session) 2022 2 #:cache #t))

(define lines
  (string-split puzzle-input "\n"))

(define rounds
  (map
   (lambda (l) (string-split l " "))
   lines))

(define (convert-one v)
  (cond
    [(equal? v "A") rock]
    [(equal? v "B") paper]
    [(equal? v "C") scissors]
    [(equal? v "X") rock]
    [(equal? v "Y") paper]
    [(equal? v "Z") scissors]
    [else 'lizard-spock]))

(define part-one-input
  (map
   (lambda (round) (list (convert-one (first round)) (convert-one (second round))))
   rounds))

; (modulo (- theirs mine) 3)
; 0 = draw
; 2 = win
; 1 = lose

;0 0  Draw
;1 1
;2 2
;
;0 1  Win
;1 2
;2 0
;
;0 2  Lose
;1 0
;2 1



(define (score-round round)
  (define diff (modulo (- (mine round) (theirs round)) 3))
  (define score
    (cond
    [(eq? diff 0) 3] ; draw
    [(eq? diff 1) 6] ; win
    [(eq? diff 2) 0] ;lose
    ))
  (define bonus (+ (mine round) 1))
  (+ score bonus))

; part 1 answer
(apply +
 (map
 score-round
 part-one-input))


