using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardObject { 
    
	private Vector2 startPos;
	public bool traversable = false;

    public BoardObject(Vector2 startPos){
        this.startPos = startPos;
    }

	//returns list of all positions object takes at time t
	public virtual List<Vector2> getPositionAtTime(int t){
//		if (startPos == null) {
//            return null;
//		}
//		Debug.Log("Gulio!");
		List<Vector2> story = new List<Vector2> ();
        story.Add(startPos);
		return story;
	}

	//returns whether or not the BoardObject is still on the board
	public bool onBoardAtTime(int t){
//		if (startPos == null) {
//            return false;
//		}
		return true;
	}

	//add position to story
	public virtual void moveTo(Vector2 pos){
	}



	public virtual bool isLethal() {
		return false;
	}

	public virtual Color getColor(){
		return Color.clear;
	}

	public virtual List<string> getSpriteInPositionAtTime(Vector2 pos, int t) {
		return new List<string>();
	}

}