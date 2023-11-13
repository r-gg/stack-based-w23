
 
\    The board is an array of lenght 9 with entries:
\   0: empty
\   1: player 1 (x)
\   2: player 2 (o)

\ The layout of the board is :
\ 0 1 2
\ 3 4 5
\ 6 7 8

create board 3 3 * cells allot

\ initializing board to zeroes
: init-board
    board 3 3 * cells 0 fill
;
\ displaying board

: drawboard ( b -- b )
    ." #################################################" cr
    0 3 swap do \ j represents the row
        0 5 swap do 
            0 3 swap do \ i represents the column
                .\" #\t" 
                2 j = if \ j because it is the second nested loop
                    board k 3 * i + cells  + @ 1 = if 
                        ." x" 
                    endif
                    board k 3 * i + cells  + @ 2 = if 
                        ." o" 
                    endif
                endif
                \ ." x" 
                .\" \t"     
            loop
            ." #"
            cr    
        loop
        
        ." #################################################" cr
    loop
;

: draw-sample-board ( b -- b )
    ." #################################################" cr
    0 3 swap do \ k represents the row
        0 5 swap do 
            0 3 swap do \ i represents the column
                .\" #\t" 
                2 j = if \ j because it is the second nested loop
                    k 3 * i + .
                endif
                .\" \t"     
            loop
            ." #"
            cr    
        loop
        
        ." #################################################" cr
    loop
;

\ Set a value at a specific position in the board (zero indexed!)
: set-value ( value row col -- )
  swap 3 * +  \ Calculate the offset in cells
  board swap cells + ! \ Store the value at the calculated offset
;


init-board

\ Example usage:
1 1 2 set-value  \ Set the value 1 at row 1, column 2 (zero indexed!)
2 2 2 set-value  \ Set the value 2 at row 2, column 2 (zero indexed!)

\ here are some examples for setting values in the board



drawboard
draw-sample-board

\ TODO: as a player either input one of the numbers 0-8 or q to quit or s to show sample board

