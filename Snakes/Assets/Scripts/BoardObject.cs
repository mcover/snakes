using UnityEngine;
using System.Collections;

public class BoardObject : MonoBehaviour { 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//needs initializer for startPos
	private Vector2 startPos;

	//returns list of all positions object takes at time t
	public ArrayList<Vector2> getPositionAtTime(int t){
		if (startPos == null) {
			fprintf (stderr, "position of the gameObject is null .\n");
			exit (-1);
		}
		// I bet there is a better way of creating story here
		Vector2[] storyArray = new Vector2[][] {startPos};
		ArrayList story = new ArrayList(storyArray);
		return story;
	}

	//returns whether or not the BoardObject is still on the board
	public bool onBoardAtTime(int t){
		if (startPos == null) {
			fprintf(stderr, "startPos is null;.\n");
			exit(-1);
		}
		return true;
	}
}
