using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : BoardObject {

	private Color color;
    public new readonly bool traversable = true;

	public Goal(Vector2 startPos,Color color) : base(startPos)
    {
        this.color = color;
    }

	new public Color getColor(){
		return color;
	}

    //implement class methods
}
