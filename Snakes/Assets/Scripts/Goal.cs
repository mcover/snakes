using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : BoardObject {

	private Color color;
//    public new bool traversable = true;

	public Goal(Vector2 startPos,Color color) : base(startPos)
    {
		this.traversable = true;
        this.color = color;
    }

	public override Color getColor(){
		Debug.Log ("Goal color called");
		return color;
	}

    //implement class methods
}
