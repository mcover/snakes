using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//statically write in data
		int mapWidth = 7;
		int mapHeight = 7;
		gameTime = 0;
		map = new Map (gameTime, mapWidth, mapHeight);
		puzzleObjects = null;
		allSnakes = null;
		activeSnake = allSnakes[0];
		pastSnakes = new List<Snake>(new Snake[] {});
		updateBoard ();
	}

	// Update is called once per frame
	void Update () {
		// Button for snake selection
		//		if(Input.GetButtonDown("snakeSelect"))
		//		{
		// Probably something like buttonID
		//			Debug.Log(Input.mousePosition);
		//		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			Debug.Log("up key was released");
			move (activeSnake, Vector2.up);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			Debug.Log("down key was released");
			move (activeSnake, Vector2.down);
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			Debug.Log("left key was released");
			move (activeSnake, Vector2.left);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			Debug.Log("right key was released");
			move (activeSnake, Vector2.right);
		}
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			Debug.Log("1 key was released");
			// Possibly refresh game / refresh snake
		}

	}

	private Map map;
	private int mapWidth, mapHeight;
	private int gameTime; //current timestep of game
	private Snake activeSnake;
	private List<Snake> pastSnakes;
	private List<Snake> allSnakes; //list of all snakes that exist in the puzzle
	private List<BoardObject> puzzleObjects; //list of all objects inside the puzzle

	//take keyboard input somehow
	//check if moving the current snake to the new position would be a valid move
	//move the snake
	//if the move took place, call update board

	void move(BoardObject obj, Vector2 direction){
		Vector2 newPos = obj.getPositionAtTime(gameTime)[-1] + direction; 
		if (canMove(obj, newPos)){
			obj.moveTo (newPos);
			updateBoard ();
		}
	}

	// Check the object obj is allowed to move in position pos
	bool canMove(BoardObject obj, Vector2 pos){
		// Find the last position that this object occupied, (possibly the same one if gameTime = 0)
		int previousIndex = Math.Max(0, gameTime - 1);
		Vector2 previousPos = obj.getPositionAtTime (previousIndex)[-1];
		// Check if the position is traversable, and check if the object is walking into itself
		if (map.isTraversable(pos) && previousPos != pos){
			return true;
		}
		return false;
	}

	//increase timestep, update board visually
	void updateBoard(){
		map = new Map (gameTime, mapWidth, mapHeight);
		putObjs ();
		parseCheckTiles ();
		//TODO parse check tiles
		//TODO trigger the drawing of the board?
	}

	//put all objects in the map at the current time
	void putObjs(){
		map.put (activeSnake); //put in the active snake
		foreach (var obstacle in puzzleObjects){
			map.put (obstacle); // put in all obstacles
		}
		foreach (var snake in pastSnakes){
			map.put(snake); // put in the snakes you've already moved
		}
	}
		
	//parses map.checkTiles(), runs any animations/game logic needed
	void parseCheckTiles(){
		List<Vector2> collisions = new List<Vector2> ();

		List<BoardEvent> boardEvents = map.checkTiles ();
		foreach (var boardEvent in boardEvents){
			var obj0 = boardEvent.getObjectPair ()[0];
			var obj1 = boardEvent.getObjectPair ()[1];

			if ((obj0 == activeSnake && obj1.isLethal ()) || (obj1 == activeSnake && obj0.isLethal ())) {
				collision ();
			} else if (obj0 is Goal && obj1 is Snake && obj0.getID () == obj1.getID ()) {
				reachedExit ();
			} else if (obj1 is Goal && obj0 is Snake && obj0.getID () == obj1.getID ()) {
				reachedExit ();
			} else {
				//this means a snake has collided with something that is not its goal, do nothing
			}
		}

	}

	//
	void collision(){
		//TODO Draw collision on the board, give feedback for the error, and wait a few seconds
		//Go back in time to the beginning of the game, mantaining the activeSnake
		rollBackTime();
	}

	//
	void reachedExit(){
		//increase timestep
		//updateboard
		//check if all snakes are on board
	}

	//Reset the gameTime to 0 and reset the story of the activeSnake to 0, then redraw the board with updateBoard
	void rollBackTime(){
		gameTime = 0;
		activeSnake.resetStory ();
		updateBoard ();
	}

}
