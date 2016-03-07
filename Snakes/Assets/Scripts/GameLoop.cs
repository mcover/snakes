using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {

	// Use this for initialization
	void Start () {

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
			updateBoard ();
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			Debug.Log("down key was released");
			updateBoard ();
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			Debug.Log("left key was released");
			updateBoard ();
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			Debug.Log("right key was released");
			updateBoard ();
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

	void move(string direction){
	}

	//increase timestep, update board visually
	void updateBoard(){
		map = Map (gameTime, mapWidth, mapHeight);
		putObjs ();
		parseCheckTiles ();
		//parse check tiles
	}

	//put all objects in the map at the current time
	void putObjs(){
		map.put (activeSnake); //put in the active snake
		foreach (var obstacle in puzzleObjects){
			map.put (obstacle); // put in all obstacles
		}
		foreach (object snake in pastSnakes){
			map.put(snake); // put in the snakes you've already moved
		}
	}


	//parses map.checkTiles(), runs any animations/game logic needed
	void parseCheckTiles(){
		map.checkTiles ();
	}




}
