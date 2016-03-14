using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Snake : BoardObject {

	//unique ID for snake, must match goal
	private int length;
	private Vector2 startPos;
    public bool exitInStory = false;
	private List<Vector2> story;
	private List<Vector2> directionStory;
    public new readonly bool traversable = true;
	private Color color;

	public Snake(Vector2 startPos, int length, Vector2 heading, Color color) : base(startPos)
    {
        this.length = length;
		this.directionStory = new List<Vector2>();
		// Initialize the directionStory with the initial heading
		this.directionStory.Add (heading);
		this.startPos = startPos;
        this.story = new List<Vector2>();
        this.story.Add(startPos);
		this.color = color;
//		Debug.Log (this.color);
    }
    //return position of the head
    public Vector2 getHead(){
		if (story == null || story.Count == 0) {
		}
        return story[story.Count - 1];
	}

	//add position to story, and newDirection to directionStory
	// NOTE: If we want to teleport the snake to a cell far away, we will need to pass a new heading
	 public override void moveTo(Vector2 pos){
//		if (story == null || story.Count == 0) {
//            
//		}
//		if (pos == null) {
//            
//		}
		// Compute new direction of the snake and add it to the directionStory  
		Vector2 prevPos = this.getHead ();
		Vector2 newDirection = pos - prevPos;
		directionStory.Add (newDirection);
		// Add position to story
		story.Add(pos);
	}

	//return list of positions snake occupies at given time
	//Note: assuming t starts at 0 at that snakes are instantiated with nonempty story
	public override List<Vector2> getPositionAtTime(int t){
		if (story == null || story.Count == 0) {
            return null;
		}
        
		int tailIndex = Math.Max(0, t + 1 - length);
        if (tailIndex < story.Count) {
            return story.GetRange(tailIndex, Math.Min(t + 1 - tailIndex, story.Count - tailIndex));
        } else {
            return new List<Vector2>();
        }
	}

	//returns whether or not the snake is still on the board
	// Note: only return false when the entire snake left the board
	public new bool onBoardAtTime(int t){
		if (story == null || story.Count == 0) {
		}
		bool isOnBoard =  ((story.Count + length <= t));
		return isOnBoard;
	}

    //implement inherited vars and methods

	new public bool isLethal() {
		return true;
	}

	public override Color getColor() {
		return color;
	}

	// Reset the story of the snake to the original position. (Used to rollBackTime)
	public void resetStory(){
	    story = new List<Vector2>();
	    story.Add(startPos);
        exitInStory = false;
	}
		
	//Given a position in the game board and a time
	//Returns list length two to draw the correct snake sprites
	public override List<string> getSpriteInPositionAtTime(Vector2 pos, int t){
		//get the snake position at time
		string tileType;
		string orientation;
		List<Vector2> currentPositions = getPositionAtTime(t);

		int index = currentPositions.FindIndex(a => a == pos);

		//get tile type
		if (index == 0 && currentPositions.Count == length) {
			tileType = "TAIL";
		} else if (index == (currentPositions.Count - 1)) {
			tileType = "HEAD";
		} else {
			Vector2 middlePiece = currentPositions [index - 1] + currentPositions [index + 1];
			if (middlePiece.x != 0 && middlePiece.y != 0) {
				tileType = "CORNER";
			} else {
				tileType = "STRAIGHT";
			}
		}

		//whatever index it is, we go that far back into directionStory to get direction
		Vector2 orientationVector = directionStory[t - index];
		if (orientationVector.Equals (Vector2.up)) {
			orientation = "UP";
		} else if (orientationVector.Equals (Vector2.right)) {
			orientation = "RIGHT";	
		} else if (orientationVector.Equals (Vector2.down)) {
			orientation = "DOWN";
		} else {
			orientation = "LEFT";
		}

		return new List<string>(new string[] { tileType, orientation });
	}

}
