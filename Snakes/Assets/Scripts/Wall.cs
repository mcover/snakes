using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : BoardObject {

    public Wall(Vector2 startPos) : base(startPos)
    {
    }

    //implement inherited vars and methods

	public override List<string> getSpriteInPositionAtTime(Vector2 pos, int t){
		return new List<string>(new string[] { "WALL", "UP" });
	}
}
