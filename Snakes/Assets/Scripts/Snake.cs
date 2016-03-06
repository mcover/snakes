using UnityEngine;
using System;
using System.Collections;

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
	private ArrayList<Vector2> story;
	private string heading;


	//return position of the head
	public Vector2 getHead(){
		if (story == null || story.isEmpty()) {
			fprintf(stderr, "story is null or empty;.\n");
			exit(-1);
		}
		return story.get(story.Count - 1)
	}

	//add position to story
	void moveTo(Vector2 pos){
		if (story == null || story.isEmpty()) {
			fprintf(stderr, "story is null or empty;.\n");
			exit(-1);
		}
		if (pos == null) {
			fprintf(stderr, "Position is null.\n");
			exit(-1);
		}
		story.Add(pos);
	}

	//return list of positions snake occupies at given time
	//Note: assuming t starts at 0 at that snakes are instantiated with nonempty story
	public ArrayList<Vector2> getPositionAtTime(int t){
		if (story == null || story.isEmpty()) {
			fprintf(stderr, "story is null or empty;.\n");
			exit(-1);
		}
		if (t > story.Count){
			fprintf(stderr, "Inconsistency between query time and story length\n");
			exit(-1);
		}
		int tailIndex = Math.Max(0, t - length);
		return story.GetRange(tailIndex, t + 1)
	}

	//returns whether or not the snake is still on the board
	// Note: only return false when the entire snake left the board
	public bool onBoardAtTime(int t){
		if (story == null || story.isEmpty()) {
			fprintf(stderr, "story is null or empty;.\n");
			exit(-1);
		}
		bool isOnBoard =  (story.Count + length <= t);
		return isOnBoard;
	}

	//implement inherited vars and methods

}
