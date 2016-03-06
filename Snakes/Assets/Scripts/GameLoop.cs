using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	private Map map;
	private int gameTime; //current timestep of game
	private Snake activeSnake;
	private List<Snake> pastSnakes;
	private List<Snake> allSnakes; //list of all snakes that exist in the puzzle
	private List<BoardObject> puzzleObjects; //list of all objects inside the puzzle


	//take keyboard input somehow
	//check if moving the current snake to the new position would be a valid move
	//move the snake
	//if the move took place, call update board
	/*
	void move(string direction){
	}
	*/

	//increase timestep, update board visually
	void updateBoard(){
		//clear map
		//put map
		//parse check tiles
	}

	//parses map.checkTiles(), runs any animations/game logic needed
	void parseCheckTiles(){
	}




}
