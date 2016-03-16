using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Snake : BoardObject {

	//unique ID for snake, must match goal
	private int length;
	private Vector2 startPos;
	private Vector2 heading;
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
		this.heading = heading;
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

	//return list of direction snake had at given time
	//Note: assuming t starts at 0 at that snakes are instantiated with nonempty story
	public new List<Vector2> getDirectionAtTime(int t){
		if (directionStory == null || directionStory.Count == 0) {
			return null;
		}

		int tailIndex = Math.Max(0, t + 1 - length);
		if (tailIndex < directionStory.Count) {
			return directionStory.GetRange(tailIndex, Math.Min(t + 1 - tailIndex, directionStory.Count - tailIndex));
		} else {
			return new List<Vector2>();
		}
	}

    //returns if the head is at the coord at the time provided
    public bool headAtCoordAtTime(Vector2 coord, int t) {
        if (t >= story.Count) {
            return false;
        } else {
            int x = Convert.ToInt32(coord.x);
            int y = Convert.ToInt32(coord.y);
            int head_x = Convert.ToInt32(story[t].x);
            int head_y = Convert.ToInt32(story[t].y);
            if ((x == head_x) && (y == head_y)) {
                return true;
            } else {
                return false;
            }
        }
    }

	//returns whether or not the snake is still on the board
	// Note: only return false when the entire snake left the board
	public new bool onBoardAtTime(int t){
		
		bool isOnBoard =  ((story.Count + length) > t);
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
		story = new List<Vector2>() {startPos};
		directionStory =  new List<Vector2>() {heading};
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
	
		List<Vector2> currentDirections = getDirectionAtTime(t);

		Vector2 entryDirection = currentDirections [index];
		Vector2 exitDirection = currentDirections[Math.Min(index + 1, currentDirections.Count - 1)];
//
		//get tile type
		//Debug.Log ("what t: " + t);
		//Debug.Log ("what cP: " + currentPositions[index]);
		//Debug.Log ("pos: " + pos);
//		Debug.Log ("what directionStoryatT: " + directionStory[index]);
		Vector2 orientationVector = new Vector2(0,0);
		int currentLength = currentPositions.Count;
		int prevMoveIndex = directionStory.Count - currentLength + 1;
		int currentIndex = directionStory.Count - currentLength;
		if (index == 0 && (currentPositions.Count == length || t > length)) {
			tileType = "TAIL";
			orientationVector = currentDirections[Math.Min(1,currentDirections.Count - 1)];
		} else if (index == (currentPositions.Count - 1) && (t < length || currentPositions.Count == length )) {
			tileType = "HEAD";
			orientationVector = currentDirections[currentDirections.Count-1];
		} else {
			
			// Dot product is 0 if and only if the two vectors are perpendicular
			if (Vector2.Dot(entryDirection, exitDirection) == 0) {
				tileType = "CORNER";
				if (((entryDirection == Vector2.left) && (exitDirection == Vector2.up))
					|| ((entryDirection == Vector2.down) && (exitDirection == Vector2.right))) {
						orientationVector = Vector2.up;
				} else if (((entryDirection == Vector2.down) && (exitDirection == Vector2.left))
					|| ((entryDirection == Vector2.right) && (exitDirection == Vector2.up))){
						orientationVector = Vector2.left;
				} else if (((entryDirection == Vector2.up) && (exitDirection == Vector2.right))
					|| ((entryDirection == Vector2.left) && (exitDirection == Vector2.down))){
						orientationVector = Vector2.right;
				} else if (((entryDirection == Vector2.up) && (exitDirection == Vector2.left))
					|| ((entryDirection == Vector2.right) && (exitDirection == Vector2.down))){
						orientationVector = Vector2.down;
					}
				}
			else {
				tileType = "STRAIGHT";
				orientationVector = entryDirection;
			}
		}
		//Debug.Log ("what tileType: " + tileType);
		//whatever index it is, we go that far back into directionStory to get direction

		//		Vector2 orientationVector = directionStory[t - index];
//		Vector2 orientationVector = directionStory[index];
		if (orientationVector.Equals (Vector2.up)) {
			orientation = "UP";
		} else if (orientationVector.Equals (Vector2.right)) {
			orientation = "RIGHT";	
		} else if (orientationVector.Equals (Vector2.down)) {
			orientation = "DOWN";
		} else {
			orientation = "LEFT";
		}
		//Debug.Log ("CURRENT POSITION IS  " + pos);
		//Debug.Log ("GET DIRECTION AT TIME: " + currentDirections[0]);
		//Debug.Log ("TILETYPE IS " + tileType);
		//Debug.Log ("ORIENTATION VECTOR IS " + orientationVector);
		//Debug.Log ("ORIENTATION IS " + orientation);
		return new List<string>(new string[] {tileType, orientation});
	}

	public new int getLength(){
		return length;
	}
}
