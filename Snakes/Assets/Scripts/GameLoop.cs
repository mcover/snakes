using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour {
    public Dictionary<string, Color> color_map = new Dictionary<string, Color>() {
        {"red", Color.red },
        {"green", Color.green },
        {"yellow", Color.yellow },
        {"purple", Color.magenta},
        {"blue", Color.blue },
    };
	public GameObject tileReference;
	//initialization of the snake buttons based upon snake colors
	private void setSnakeSelectionPanel(){
		List<Color> buttonColors = new List<Color> ();
		foreach (Snake snake in allSnakes) {
			buttonColors.Add (snake.getColor ());
		}
        this.GetComponent<UIManager>().SetColors(buttonColors); //grabs UI manager and calls the fuction which sets the colors.
	}

	//updates selection panel by giving a list of booleans
	//false: snake isn't complete
	private void updateSnakeSelectionPanel(){
		List<bool> complete = new List<bool> (new bool[pastSnakes.Count]);//I assume that list (int x) constructor creates list of size x set to default (false) bool vals
		for (int i = 0; i < allSnakes.Count; i++) {
			foreach (Snake snake in pastSnakes){
				if (snake.getColor () == allSnakes [i].getColor ()) {
					complete [i] = true;
				}
			}
		}
        this.GetComponent<UIManager>().UpdateSnakeButtons(complete);
    }

	//
    private void enableSelectionPanel(){
        this.GetComponent<UIManager>().DisableSnakeSelection();
        //run animation
    }

	private void disableSelectionPanel() {
		this.GetComponent<UIManager>().EnableSnakeSelection();
    }

    public void loadLevel(int level)
    {	
		Debug.Log ("level" + level);
        TextAsset txt = (TextAsset)Resources.Load("levels/level" + level.ToString(), typeof(TextAsset));
        string levelString = txt.text;
        string[] objectStrings = levelString.Replace("\r", "").Split('\n');
        allSnakes = new List<Snake>(); //list of all snakes that exist in the puzzle
        puzzleObjects = new List<BoardObject>(); //list of all other objects inside the puzzle
        gameTime = 0;
        keyboardLock = false;

        foreach (string objectString in objectStrings) {
            string[] tokens = objectString.Split(',');
            bool noParseErrors = true;
            if (tokens[0] == "dims") {
                noParseErrors = noParseErrors
                    & int.TryParse(tokens[1], out mapWidth)
                    & int.TryParse(tokens[2], out mapHeight);
                if (!noParseErrors) {
                    Debug.LogError("ERROR PARSING DIMENSIONS" + objectString);
                }
            } else if (tokens[0] == "snake") {
                int startX;
                int startY;
                int length;
                int headingX;
                int headingY;
                string colorString = tokens[6];
                noParseErrors = noParseErrors
                    & int.TryParse(tokens[1], out startX)
                    & int.TryParse(tokens[2], out startY)
                    & int.TryParse(tokens[3], out length)
                    & int.TryParse(tokens[4], out headingX)
                    & int.TryParse(tokens[5], out headingY);
                Vector2 startPos = new Vector2(startX, startY);
                Vector2 heading = new Vector2(headingX, headingY);
                Color color = color_map[colorString];
                if (noParseErrors) {
                    allSnakes.Add(new Snake(startPos, length, heading, color));
                } else {
                    Debug.LogError("ERROR PARSING SNAKE" + objectString);
                }
            } else if (tokens[0] == "goal") {
                int startX;
                int startY;
                string colorString = tokens[3];
                noParseErrors = noParseErrors
                    & int.TryParse(tokens[1], out startX)
                    & int.TryParse(tokens[2], out startY);
                Vector2 startPos = new Vector2(startX, startY);
                Color color = color_map[colorString];

                if (noParseErrors) {
                    puzzleObjects.Add(new Goal(startPos, color));
                }
                else {
                    Debug.LogError("ERROR PARSING GOAL" + objectString);
                }
            } else if (tokens[0] == "wall") {
                int startX;
                int startY;
                noParseErrors = noParseErrors
                    & int.TryParse(tokens[1], out startX)
                    & int.TryParse(tokens[2], out startY);
                Vector2 startPos = new Vector2(startX, startY);

                if (noParseErrors) {
                    puzzleObjects.Add(new Wall(startPos));
                }
                else {
                    Debug.LogError("ERROR PARSING WALL" + objectString);
                }
            }
        }
		activeSnake = allSnakes [0];
        Debug.Log("active snake " + activeSnake.getColor());
        pastSnakes = new List<Snake>(new Snake[] {});
		setSnakeSelectionPanel ();
		Debug.Log ("GAME LOOP DRAW CALL 0");
		tileReference.GetComponent<Tiles>().drawEmptyBoard(mapWidth, mapHeight);
		Debug.Log ("GAME LOOP DRAW CALL 5");

		updateBoard ();
    }

	// Use this for initialization

	void Start () {
	}

	// Update is called once per frame
	void Update () {
        gameTimeLabel.text = "Time: " + gameTime;
		// Button for snake selection
		//        if(Input.GetButtonDown("snakeSelect"))
		//        {
		// Probably something like buttonID
		//            Debug.Log(Input.mousePosition);
		//        }
		if (keyboardLock) {
			return;
		}

		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			//Debug.Log("up key was released");
			move (activeSnake, Vector2.up);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			//Debug.Log("down key was released");
			move (activeSnake, Vector2.down);
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			//Debug.Log("left key was released");
			move (activeSnake, Vector2.left);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			//Debug.Log("right key was released");
			move (activeSnake, Vector2.right);
		}
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			//Debug.Log("1 key was released");
			// Possibly refresh game / refresh snake
		}

	}

	public Text gameTimeLabel;

	private Map map;
	private int mapWidth, mapHeight;
	private bool keyboardLock;
	private int gameTime; /*{
		set { gameTimeLabel.text = gameTime.ToString ();}
		get { return gameTime; }
	}*/ //current timestep of game
	private Snake activeSnake;
	private List<Snake> pastSnakes;
	private List<Snake> allSnakes; //list of all snakes that exist in the puzzle
	private List<BoardObject> puzzleObjects; //list of all objects inside the puzzle

	//take keyboard input somehow
	//check if moving the current snake to the new position would be a valid move
	//move the snake
	//if the move took place, call update board
	void move(BoardObject obj, Vector2 direction){
        disableSelectionPanel();
        //		Debug.Log ("MOVING SNAKE");
        Debug.Log(obj + " " + obj.getColor());
		List<Vector2> storyAtTime = obj.getPositionAtTime(gameTime);
		Vector2 newPos = storyAtTime[storyAtTime.Count - 1] + direction;
		if (canMove(obj, newPos)){
//			Debug.Log ("OBJECT CAN MOVE");
			// if game time is zero, this is the special case
			// reset the past snake if it's in there
			// disable the snake selection panel
			if (gameTime == 0 && activeSnake != null) {
//				Debug.Log ("special case of game time = 0");
				confirmActiveSnake ();
				//NOTE: Commented out for debugging
//				disableSelectionPanel ();
				// TODO disable the snake selection panel
			}
			gameTime++;
            gameTimeLabel.text = "Time: " + gameTime; 
//			Debug.Log ("Attemt to move to new position: " + newPos	);
//		Debug.Log ("The object is: " + obj);
			obj.moveTo (newPos);

//			Debug.Log ("Snake head position " + (Snake(obj)).getHead() );
			updateBoard ();
		}
	}


	// Check the object obj is allowed to move in position pos
	bool canMove(BoardObject obj, Vector2 pos){
//		Debug.Log ("Can move being called");
		// Find the last position that this object occupied, (possibly the same one if gameTime = 0)
		int previousIndex = Math.Max(0, gameTime - 1);
		List<Vector2> storyAtPrevTime = obj.getPositionAtTime (previousIndex);
		Vector2 previousPos = storyAtPrevTime[storyAtPrevTime.Count - 1];
        // Check if the position is traversable, and check if the object is walking into itself
		if (map.isTraversable(pos) && (previousPos != pos)){
			return true;
		}
        return false;
	}

	//increase timestep, update board visually
	void updateBoard(){
		map = new Map (gameTime, mapWidth, mapHeight);
		putObjs ();
		parseCheckTiles ();
        //TODO trigger the drawing of the board
        //		tileReference.GetComponent<Tiles>().drawEmptyBoard(mapWidth, mapHeight);
		tileReference.GetComponent<Tiles>().drawMap(map);
	}

	//put all objects in the map at the current time
	void putObjs(){
		map.put (activeSnake); //put in the active snake
        Debug.Log("PUT ACTIVE SNAKE " + activeSnake + " " + activeSnake.getColor());
//		Debug.Log("HERE WE ARE " + map.get(new Vector2(1,2)).Count);
		foreach (BoardObject obstacle in puzzleObjects){
//			Debug.Log ("put obstacle "+ obstacle.getPositionAtTime(gameTime)[0].x + " " + obstacle.getPositionAtTime(gameTime)[0].y);
			map.put (obstacle); // put in all obstacles
		}
		foreach (var snake in pastSnakes){
			map.put(snake); // put in the snakes you've already moved
		}

	}

	//parses map.checkTiles(), runs any animations/game logic needed
	void parseCheckTiles(){
//
//		Debug.Log ("Checking tiles");
//		Debug.Log ("snake head is at " + activeSnake.getHead ());
//		Debug.Log ("map position 1,2" + map.map [1, 2].Count);

		Vector2 exitPosition = new Vector2(-1,-1);
		List<BoardEvent> boardEvents = map.checkTiles ();
        //		Debug.Log ("Board event list size = " + boardEvents.Count);
		foreach (var boardEvent in boardEvents){
			BoardObject obj0 = boardEvent.getObjectPair ()[0];
			BoardObject obj1 = boardEvent.getObjectPair ()[1];
            //Debug.Log ("Getting board event");
			if ( (obj0 == activeSnake && (obj1 is Snake)) 
                || (obj1 == activeSnake && (obj0 is Snake))
                ) {
				collision (boardEvent.getPos ());
                return;
			} else if (obj0 is Goal && obj1 is Snake && ((Goal)obj0).getColor ().Equals (((Snake)obj1).getColor ())) {
				exitPosition = boardEvent.getPos ();
                ((Snake)obj1).exitInStory = true;
			} else if (obj1 is Goal && obj0 is Snake && obj0.getColor ().Equals  (obj1.getColor ())) {
				exitPosition = boardEvent.getPos ();
                ((Snake)obj0).exitInStory = true;
            } else {
				//this means a snake has collided with something that is not its goal, do nothing
			}
        }

		if (exitPosition.x != -1) { //-1 is magic value as there can never be negative position
			reachedExit (exitPosition);
			Debug.Log ("reached exit");
		}
	}

	//
	void collision(Vector2 collCoord){
		//TODO Draw collision on the board, give feedback for the error, and wait a few seconds
		//Go back in time to the beginning of the game, mantaining the activeSnake
		rollBackTime();
        enableSelectionPanel();
	}

	//
	void reachedExit(Vector2 exitCoord){
		keyboardLock = true;
        Debug.Log("CHECKCHECKCHECK " + snakesStillOnBoardAtTimeStep(gameTime) + " " + gameTime);
		//if (snakesStillOnBoardAtTimeStep (gameTime)) {
        if (!activeSnake.exitInStory) {
            Debug.Log("CHECKCHECKCHECK2 " + snakesStillOnBoardAtTimeStep(gameTime) + " " + gameTime);
            //gameTime++;
            keyboardLock = false;
            //updateBoard ();
            Debug.Log("FIRST EXIT IF");
		} else {
            Debug.Log("SECOND EXIT IF");
			pastSnakes.Add (activeSnake);

			//you've won the level
			if (pastSnakes.Count == allSnakes.Count) {
				gameWin ();
			} else {
				InvokeRepeating ("rewind", 0.0F, 0.25F);
//				gameTime = 0;
                activeSnake = allSnakes.Find(x => !x.exitInStory);
                updateBoard ();
                int snakeIndex = allSnakes.IndexOf(activeSnake);
                this.GetComponent<UIManager>().activeSnakeFeedback(snakeIndex);
                updateSnakeSelectionPanel ();
				//Debug.Log("Resetting active snake!");
//				keyboardLock = true;
			}
            enableSelectionPanel();
		}
		//updateboard
	}

	void gameWin (){
		Debug.Log ("Game has been won");
        //enable win prompt
        this.GetComponent<UIManager>().WonLevel();
	}

	bool snakesStillOnBoardAtTimeStep(int t) {
		bool stillOn = !activeSnake.exitInStory;
        Debug.Log("STILL ON: " + stillOn + " " + t);
			foreach (Snake snake in pastSnakes){
				stillOn = stillOn || snake.onBoardAtTime(t);
			}
		return stillOn;
	}

	//Reset the gameTime to 0 and reset the story of the activeSnake to 0, then redraw the board with updateBoard
	void rollBackTime(){
		gameTime = 0;
        gameTimeLabel.text = "Time: " + 0;
        activeSnake.resetStory();
		updateBoard ();
	}

	void rewind(){
		if (gameTime > 0) {
			gameTime--;
            gameTimeLabel.text = "Time: " + gameTime;
			updateBoard ();
			//make noise
		} else {
			CancelInvoke ();
			keyboardLock = false;
		}
			
	}
		


	public void selectSnakeatIndex( int snakeIndex) {
		activeSnake = allSnakes [snakeIndex];
		keyboardLock = false;
        this.GetComponent<UIManager>().activeSnakeFeedback(snakeIndex);
        //Debug.Log("switching to snake" + snakeIndex);
	}

	// If the selected snake was already played, reset its story and remove it from past snakes
	public void confirmActiveSnake(){
		if (pastSnakes.Contains(activeSnake)) {
			pastSnakes.Remove (activeSnake);
			activeSnake.resetStory();
		}
	}


	//timer func
	//if (gameTime > 0) {
	//rewind();
	//}

}