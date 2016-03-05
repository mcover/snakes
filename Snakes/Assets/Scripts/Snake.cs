using UnityEngine;
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
	
	}

	//add position to story
	void moveTo(Vector2 pos){
	
	}

	//return list of positions snake occupies at given time
	public ArrayList<Vector2> getPositionAtTime(int t){
	
	}

	//returns whether or not the snake is still on the board
	public bool onBoardAtTime(int t){
	}

	//implement inherited vars and methods

}
