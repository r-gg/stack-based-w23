\   The board is an array of length 9 with entries:
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

: draw-board ( b -- b )
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

\ Print out the instructions on how to play
: show-instructions
  ." Choose the tile by typing in the number corresponding to it, sample board:" cr
;

: rc-to-index ( row column -- linear-index )
    swap 3 * +
;

\ Set a value at a specific position in the board (zero indexed!)
: set-value ( value row col -- )
  rc-to-index \ Calculate the offset in cells
  board swap cells + ! \ Store the value at the calculated offset
;

\ Example usage:
\ here are some examples for setting values in the board
\ init-board
\ 1 1 2 set-value  \ Set the value 1 at row 1, column 2 (zero indexed!)
\ 2 2 2 set-value  \ Set the value 2 at row 2, column 2 (zero indexed!)

: free ( field -- flag )
    board swap cells + @ 0 =  \ Check if the field is not empty
;

: index-to-rc ( linear-index -- row column )
  3 /mod  \ Divide the linear index by 3, giving quotient and remainder
  swap     \ Swap the quotient and remainder
;

: get-player-move ( board-entry -- )
  begin
    key 
    dup [char] s = if
        cr draw-sample-board drop
    else
        dup [char] q = if
                ." Quit chosen." cr bye
            else
                dup [char] 0 [char] 9 within if 
                    dup [char] 0 - free if 
                            [char] 0 - index-to-rc set-value exit
                        else
                            drop cr ." Field already occupied, please try again [0-8]: " cr
                        then
                else
                    drop cr ." Wrong input, please try again [0-8]: " cr
            then
        then
    then
  again
;

\ check if the three indices are of same player
: check-line ( i1 i2 i3 player -- flag flag ) { i1 i2 i3 player }
    i1 board swap cells + @ player = i2 board swap cells + @ player = i3 board swap cells + @ player = and and
;

: check-rows ( player -- flag ) { player }  
	0 7 swap +do
		i i 1 + i 2 + player check-line
	3 +loop 
	or or
	;
	
: check-cols ( player -- flag ) { player }  
	0 3 swap +do
		i i 3 + i 6 + player check-line
	1 +loop 
	or or
	;




\ Function to check if a player has won
: check-win ( player -- flag ) { player }
  \ Check rows
  player check-rows
  \ Check columns
  player check-cols or
  \ Check diagonals
  0 4 8 player check-line 
  2 4 6 player check-line or or
;

: game-loop
  init-board
  show-instructions
  draw-sample-board
  9 0 do
    i 2 mod 1+ get-player-move
    draw-board
    i 3 > if
        i 2 mod 1+ dup check-win if cr ." Player " . ." has won" cr leave else drop then
        i 8 = if cr ."  Stalemate" cr leave then
    then
  loop
;

: up2 ( n1 n2 -- )
       +do
         i .
       2 +loop ;
10 0 up2

game-loop
