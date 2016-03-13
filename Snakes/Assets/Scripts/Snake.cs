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
}
