using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	private int time; //time step map represents
	private ArrayList<ArrayList<ArrayList<BoardObject>>> map; //2d array of arrays of boardObjects

	//gathers and returns a list of tiles where an event must have occurred
	//objects in array are of the format:
	//vector2 position
	//arraylist BoardObject
	public ArrayList<Vector2> checkTiles(){
		
	}

	//takes any board object, places it at the positions it occupies within the map
	public void put(BoardObject obj){
	
	}

	public bool isTraversable(Vector2 pos){
	}


}
