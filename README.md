Project Name – ZoneCapture

 Core Gameplay Mechanics
•	As the player moves, a trail (wall) is created behind them.
•	The player loses a life if:
o	An enemy touches the player, OR
o	An enemy touches the trail being drawn.
•	When the player completes a loop, the enclosed area gets captured.
•	Enemies inside the captured area are destroyed.
•	The player progresses to the next level after capturing the required percentage of the grid.

Objectives
•	Capture at least 80% of the playable area to complete the level.
•	Avoid enemies while drawing trails.
•	Use power-ups strategically to survive longer.

Level Design
•	The grid system is dynamic and resolution-independent, meaning:
o	It adapts to different mobile screen sizes automatically.
o	Width and height can also be fixed or configured if required.

Player Controls
•	Desktop / Editor: Arrow Keys
•	Mobile: Floating Joystick (touch-based control)

Enemies
•	Two types of enemies:
o	 Slow-moving enemies (3 units)
o	 Fast-moving enemy (1 unit)

•	Behavior:
o	Move within the grid
o	Bounce off walls
•	Enemies are spawned only in valid empty grid cells (not walls or captured areas)

Power-Up System
•	A power-up spawns every 10 seconds (configurable from Inspector).
•	Effect:
o	Slows down or freezes enemies temporarily.
•	Power-ups spawn only in empty playable areas.


Progressive Difficulty (Rounds System)
•	The game supports multi-round progression:
o	Each new round increases difficulty:
	 +1 Enemy added
	 Enemy speed increases
	 Capture requirement increases by +3%
•	Example:
o	Round 1 → 80%
o	Round 2 → 83%
o	Round 3 → 86%
•	After completing all rounds, the game can restart or loop.
•	Number of rounds can be easily modified via RoundManager (Inspector).
