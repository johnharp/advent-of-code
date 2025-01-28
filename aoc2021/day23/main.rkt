#lang racket
(require "move.rkt")
(require "state.rkt")
(require "filters.rkt")


#|

+-------------------------------------------+
| 0   1   2   3   4   5   6   7   8   9  10 |
+-----+  11   |  13   |  15   |  17   +-----+
      |  12   |  14   |  16   |  18   |
      +-------------------------------+
|#

(define initial-state (string->state "...........BACDBCDA"))
(define s initial-state)
;(define s2 (string->state "..............DBCDA"))
(define m (valid-moves s))
(display-state s)


