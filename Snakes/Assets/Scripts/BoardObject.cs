using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardObject { 
    
	private Vector2 startPos;
    public readonly bool traversable = false;

    public BoardObject(Vector2 startPos){
        this.startPos = startPos;
    }

	//returns list of all positions object takes at time t
	public List<Vector2> getPositionAtTime(int t){
		if (startPos == null) {
            return null;
		}
		List<Vector2> story = new List<Vector2> ();
        story.Add(startPos);
		return story;
	}

	//returns whether or not the BoardObject is still on the board
	public bool onBoardAtTime(int t){
		if (startPos == null) {
            return false;
		}
		return true;
	}
}
