using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : BoardObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private string ID;
    public new readonly bool traversable = true;

    public Goal(Vector2 startPos, string ID) : base(startPos)
    {
        this.ID = ID;
    }

    //implement class methods
}
