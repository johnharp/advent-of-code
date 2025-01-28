#lang racket
(require "state.rkt")
(provide make-move
         valid-moves
         occupied-positions
         is-move-from-home
         moves-from
         valid-moves-from
         is-move-blocked
         from-val
         to-val
         num-steps)
#|
+-------------------------------------------+
| 0   1   2   3   4   5   6   7   8   9  10 |
+-----+  11   |  13   |  15   |  17   +-----+
      |  12   |  14   |  16   |  18   |
      +-------------------------------+

A move is a list of positions with the starting index first, the ending index last
therefore the start position is (first move)
end position is (last move)
number of steps to get from start to end is length - 1

|#

(define (occupied-positions state)
  (define (is-occupied pos) (not (equal? (vector-ref state pos) ".")))
  (filter is-occupied (sequence->list 19)))

(define (valid-moves-from-positions state positions)
  (cond
    [(null? positions) '()]
    [else (append
           (valid-moves-from state (first positions))
           (valid-moves-from-positions state (rest positions)))]))
  
(define (valid-moves state)
  (valid-moves-from-positions state (occupied-positions state)))


(define (moves-from-18)
  '((18 17 8 7 6 5 4 3 2 11 12)
    (18 17 8 7 6 5 4 3 2 11)
    (18 17 8 7 6 5 4 13 14)
    (18 17 8 7 6 5 4 13)
    (18 17 8 7 6 15 16)
    (18 17 8 7 6 15)
    (18 17)
    (18 17 8 7 6 5 4 3 2 1 0)
    (18 17 8 7 6 5 4 3 2 1)
    (18 17 8 7 6 5 4 3)
    (18 17 8 7 6 5)
    (18 17 8 7)
    (18 17 8 9)
    (18 17 8 9 10)))

(define (moves-from-17)
  '((17 8 7 6 5 4 3 2 11 12)
    (17 8 7 6 5 4 3 2 11)
    (17 8 7 6 5 4 13 14)
    (17 8 7 6 5 4 13)
    (17 8 7 6 15 16)
    (17 8 7 6 15)
    (17 18)
    (17 8 7 6 5 4 3 2 1 0)
    (17 8 7 6 5 4 3 2 1)
    (17 8 7 6 5 4 3)
    (17 8 7 6 5)
    (17 8 7)
    (17 8 9)
    (17 8 9 10)))

(define (moves-from-16)
  '((16 15 6 5 4 3 2 11 12)
    (16 15 6 5 4 3 2 11)
    (16 15 6 5 4 13 14)
    (16 15 6 5 4 13)
    (16 15)
    (16 15 6 7 8 17 18)
    (16 15 6 7 8 17)
    (16 15 6 5 4 3 2 1 0)
    (16 15 6 5 4 3 2 1)
    (16 15 6 5 4 3)
    (16 15 6 5)
    (16 15 6 7)
    (16 15 6 7 8 9)
    (16 15 6 7 8 9 10)))

(define (moves-from-15)
  '((15 6 5 4 3 2 11 12)
    (15 6 5 4 3 2 11)
    (15 6 5 4 13 14)
    (15 6 5 4 13)
    (15 16)
    (15 6 7 8 17 18)
    (15 6 7 8 17)
    (15 6 5 4 3 2 1 0)
    (15 6 5 4 3 2 1)
    (15 6 5 4 3)
    (15 6 5)
    (15 6 7)
    (15 6 7 8 9)
    (15 6 7 8 9 10)))

(define (moves-from-14)
  '((14 13 4 3 2 11 12)
    (14 13 4 3 2 11)
    (14 13)
    (14 13 4 5 6 15 16)
    (14 13 4 5 6 15)
    (14 13 4 5 6 7 8 17 18)
    (14 13 4 5 6 7 8 17)
    (14 13 4 3 2 1 0)
    (14 13 4 3 2 1)
    (14 13 4 3)
    (14 13 4 5)
    (14 13 4 5 6 7)
    (14 13 4 5 6 7 8 9)
    (14 13 4 5 6 7 8 9 10)))

(define (moves-from-13)
  '((13 4 3 2 11 12)
    (13 4 3 2 11)
    (13 14)
    (13 4 5 6 15 16)
    (13 4 5 6 15)
    (13 4 5 6 7 8 17 18)
    (13 4 5 6 7 8 17)
    (13 4 3 2 1 0)
    (13 4 3 2 1)
    (13 4 3)
    (13 4 5)
    (13 4 5 6 7)
    (13 4 5 6 7 8 9)
    (13 4 5 6 7 8 9 10)))

(define (moves-from-12)
  '((12 11)
    (12 11 2 3 4 13 14)
    (12 11 2 3 4 13)
    (12 11 2 3 4 5 6 15 16)
    (12 11 2 3 4 5 6 15)
    (12 11 2 3 4 5 6 7 8 17 18)
    (12 11 2 3 4 5 6 7 8 17)
    (12 11 2 1 0)
    (12 11 2 1)
    (12 11 2 3)
    (12 11 2 3 4 5)
    (12 11 2 3 4 5 6 7)
    (12 11 2 3 4 5 6 7 8 9)
    (12 11 2 3 4 5 6 7 8 9 10)))

(define (moves-from-11)
  '((11 12)
    (11 2 3 4 13 14)
    (11 2 3 4 13)
    (11 2 3 4 5 6 15 16)
    (11 2 3 4 5 6 15)
    (11 2 3 4 5 6 7 8 17 18)
    (11 2 3 4 5 6 7 8 17)
    (11 2 1 0)
    (11 2 1)
    (11 2 3)
    (11 2 3 4 5)
    (11 2 3 4 5 6 7)
    (11 2 3 4 5 6 7 8 9)
    (11 2 3 4 5 6 7 8 9 10)))

(define (moves-from-10)
  '((10 9 8 7 6 5 4 3 2 11 12)
    (10 9 8 7 6 5 4 3 2 11)
    (10 9 8 7 6 5 4 13 14)
    (10 9 8 7 6 5 4 13)
    (10 9 8 7 6 15 16)
    (10 9 8 7 6 15)
    (10 9 8 17 18)
    (10 9 8 17)
    (10 9 8 7 6 5 4 3 2 1 0)
    (10 9 8 7 6 5 4 3 2 1)
    (10 9 8 7 6 5 4 3)
    (10 9 8 7 6 5)
    (10 9 8 7)
    (10 9)))

(define (moves-from-9)
  '((9 8 7 6 5 4 3 2 11 12)
    (9 8 7 6 5 4 3 2 11)
    (9 8 7 6 5 4 13 14)
    (9 8 7 6 5 4 13)
    (9 8 7 6 15 16)
    (9 8 7 6 15)
    (9 8 17 18)
    (9 8 17)
    (9 8 7 6 5 4 3 2 1 0)
    (9 8 7 6 5 4 3 2 1)
    (9 8 7 6 5 4 3)
    (9 8 7 6 5)
    (9 8 7)
    (9 10)))

(define (moves-from-7)
  '((7 6 5 4 3 2 11 12)
    (7 6 5 4 3 2 11)
    (7 6 5 4 13 14)
    (7 6 5 4 13)
    (7 6 15 16)
    (7 6 15)
    (7 8 17 18)
    (7 8 17)
    (7 6 5 4 3 2 1 0)
    (7 6 5 4 3 2 1)
    (7 6 5 4 3)
    (7 6 5)
    (7 8 9)
    (7 8 9 10)))

(define (moves-from-5)
  '((5 4 3 2 11 12)
    (5 4 3 2 11)
    (5 4 13 14)
    (5 4 13)
    (5 6 15 16)
    (5 6 15)
    (5 6 7 8 17 18)
    (5 6 7 8 17)
    (5 4 3 2 1 0)
    (5 4 3 2 1)
    (5 4 3)
    (5 6 7)
    (5 6 7 8 9)
    (5 6 7 8 9 10)))

(define (moves-from-3)
  '((3 2 11 12)
    (3 2 11)
    (3 4 13 14)
    (3 4 13)
    (3 4 5 6 15 16)
    (3 4 5 6 15)
    (3 4 5 6 7 8 17 18)
    (3 4 5 6 7 8 17)
    (3 2 1 0)
    (3 2 1)
    (3 4 5)
    (3 4 5 6 7)
    (3 4 5 6 7 8 9)
    (3 4 5 6 7 8 9 10)
    ))

(define (moves-from-1)
  '((1 2 11 12)
    (1 2 11)
    (1 2 3 4 13 14)
    (1 2 3 4 13)
    (1 2 3 4 5 6 15 16)
    (1 2 3 4 5 6 15)
    (1 2 3 4 5 6 7 8 17 18)
    (1 2 3 4 5 6 7 8 17)
    (1 0)
    (1 2 3)
    (1 2 3 4 5)
    (1 2 3 4 5 6 7)
    (1 2 3 4 5 6 7 8 9)
    (1 2 3 4 5 6 7 8 9 10)))

(define (moves-from-0)
  '((0 1 2 11 12)
    (0 1 2 11)
    (0 1 2 3 4 13 14)
    (0 1 2 3 4 13)      
    (0 1 2 3 4 5 6 15 16)
    (0 1 2 3 4 5 6 15)
    (0 1 2 3 4 5 6 7 8 17 18)
    (0 1 2 3 4 5 6 7 8 17)
    (0 1)
    (0 1 2 3)
    (0 1 2 3 4 5)
    (0 1 2 3 4 5 6 7)
    (0 1 2 3 4 5 6 7 8 9)
    (0 1 2 3 4 5 6 7 8 9 10)))

(define (moves-from n)
  (case n
    [(0) (moves-from-0)]
    [(1) (moves-from-1)]
    [(2) '()]
    [(3) (moves-from-3)]
    [(4) '()]
    [(5) (moves-from-5)]
    [(6) '()]
    [(7) (moves-from-7)]
    [(8) '()]
    [(9) (moves-from-9)]
    [(10) (moves-from-10)]
    [(11) (moves-from-11)]
    [(12) (moves-from-12)]
    [(13) (moves-from-13)]
    [(14) (moves-from-14)]
    [(15) (moves-from-15)]
    [(16) (moves-from-16)]
    [(17) (moves-from-17)]
    [(18) (moves-from-18)]))

(define (from-val move)
  (first move))

(define (to-val move)
  (last move))

(define (num-steps move)
  (sub1 (length move)))

; is-move-blocked
; Returns:
;   #true if this move would collide with another occupied space
;   #false if the move would not collide
(define (is-move-blocked state move)
  (let* ([check-occupied (curry is-occupied state)]
         [blocked-spots (filter check-occupied (cdr move))])
    (> (length blocked-spots) 0)))

; is-move-including-locs
; Returns:
;     #true if the move passes through any location in locs
;     #false if the move does not pass through any of the given locations
(define (is-move-including-locs move locs)
  (> (length (set-intersect move locs)) 0))

; is-move-from-home
; Returns:
;     #true if the move would take an amiphod out of its home room in an illegal move
;           * moving from its home-2 position is always disallowed
;           * moving from its home-1 position (away from home-2, toward the hallway)
;             when home-2 is either empty or occupied with the correct amiphod
;     #false otherwise
(define (is-move-from-home state move)
  (let* ([moved-amiphod (vector-ref state (first move))]
         [start-pos (first move)]
         [end-pos (last move)]
         [h1-pos (home-1-pos moved-amiphod)]
         [h2-pos (home-2-pos moved-amiphod)]
         [h2-occupied-by (vector-ref state h2-pos)])
  (or
     (= start-pos h2-pos)
     (and (= start-pos h1-pos) (not (= end-pos h2-pos)) (equal? h2-occupied-by moved-amiphod))
     (and (= start-pos h1-pos) (not (= end-pos h2-pos)) (equal? h2-occupied-by ".")))))


(define (is-valid-move state move)
  (and (not (is-move-blocked state move))
       (not (is-move-from-home state move))))

(define (valid-moves-from state n)
  (filter (curry is-valid-move state) (moves-from n)))

(define (make-move state move)
  (define (loc-val loc)
    (cond
      [(= loc (first move)) "."]
      [(= loc (last move)) (vector-ref state (first move))]
      [else (vector-ref state loc)]))
  (build-vector 19 loc-val))