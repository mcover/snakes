using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Snake : BoardObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//unique ID for snake, must match goal
	private string ID;
	private int length;
	private List<Vector2> story;
	private string heading;
    public new readonly bool traversable = true;

    public Snake(Vector2 startPos, int length, string heading, string ID) : base(startPos)
    {
        this.ID = ID;
        this.length = length;
        this.heading = heading;
        this.story = new List<Vector2>();
        this.story.Add(startPos);
    }
    //return position of the head
    public Vector2 getHead(){
		if (story == null || story.Count == 0) {
		}
        return story[story.Count - 1];
	}

	//add position to story
	void moveTo(Vector2 pos){
		if (story == null || story.Count == 0) {
            
		}
		if (pos == null) {
            
		}
		story.Add(pos);
	}

	//return list of positions snake occupies at given time
	//Note: assuming t starts at 0 at that snakes are instantiated with nonempty story
	public new List<Vector2> getPositionAtTime(int t){
		if (story == null || story.Count == 0) {
            return null;
		}
		if (t > story.Count){
            return null;
		}
		int tailIndex = Math.Max(0, t - length);
        return story.GetRange(tailIndex, t + 1);
	}

	//returns whether or not the snake is still on the board
	// Note: only return false when the entire snake left the board
	public new bool onBoardAtTime(int t){
		if (story == null || story.Count == 0) {
		}
		bool isOnBoard =  (story.Count + length <= t);
		return isOnBoard;
	}

    //implement inherited vars and methods
}
