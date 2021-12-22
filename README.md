# Ecosystem
Simulation of an Ecosystem using Monogame

## Student
* Dubois Hugo : 195347
* Borbolla Alejandro : 195004

## Run
Launch the `Ecosystem` project to start the simulation.

## Framework
Our simulation is based on the Monogame framework in C#.

```python
board_weight = 
	[
		[1,    1,    1,    1,    1,    None, None, None, None],
		[1,    2,    2,    2,    2,    1,    None, None, None],
		[1,    2,    3,    3,    3,    2,    1,    None, None],
		[1,    2,    3,    4,    4,    3,    2,    1,    None],
		[1,    2,    3,    4,    5,    4,    3,    2,    1   ],
		[None, 1,    2,    3,    4,    4,    3,    2,    1   ],
		[None, None, 1,    2,    3,    3,    3,    2,    1   ],
		[None, None, None, 1,    2,    2,    2,    2,    1   ],
		[None, None, None, None, 1,    1,    1,    1,    1   ]
	]
```

The priority value change for each **move possibility**.

If no move are found, the AI will give up the game.

### Priority assigment
* Can we push an opposite marble
	* The priority changes depending where the opposite marble goes on the board. The priority increases when the opposite marble is near of the edge.
* Go away from the edge of the board
* Can we be ejected
* We check if the marble can be ejected in the future

### Checkloop
The AI will save the moves done in the past with the corresponding board. The memory size can be changed by edit the variable `MEMORY_SIZE`.

Before choosing the move, the AI will check if the move and the board hasn't already be done in the past.

In the case the game is looping, we'll decrease the priority value.

## Libraries
* sys
	* Get the arguments passed to the script with `sys.argv`
* logging
	* Make print with significant level
		* `INFO`
		* `DEBUG`
		* `WARNING`
		* `CRITICAL`
* threading
	* Use to generate thread
* json
	* Generate and read the server's requests
* random
	* `shuffle()`
		* Mixed the differents messages sent to the server
* copy
	* `deepcopy()`
		* Use to generate a copy of the current board
* socket
	* Generate a TCP client
* time
	* Calcul the function time
	* Stop the strategy if the timeout is excedeed
