using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : BoardObject {

	private string ID;
    public new readonly bool traversable = true;

    public Goal(Vector2 startPos, string ID) : base(startPos)
    {
        this.ID = ID;
    }

	new public string getID(){
		return ID;
	}

    //implement class methods
}
