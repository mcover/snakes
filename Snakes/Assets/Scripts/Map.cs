using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map {

    private int time; //time step map represents
    public List<BoardObject>[,] map; //2d array of arrays of boardObjects
    private int width;
    private int height;

    public Map(int time, int width, int height) {
        this.width = width;
        this.height = height;
        this.time = time;
        map = new List<BoardObject>[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = new List<BoardObject>();
            }
        }
    }

    //gathers and returns a list of tiles where an event must have occurred
    //objects in array are of the format:
    //vector2 position
    //arraylist BoardObject
    public List<BoardEvent> checkTiles() {
//		Debug.Log ("map check tiles");
        List<BoardEvent> events = new List<BoardEvent>();
        // double for loop --> for each location
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
				
                List<BoardObject> objs = map[i, j];
//				Debug.Log ("object list for i = " + i + " and j = " + j + " = " + objs.Count);
                // if at least one event could occur
                if (objs.Count >= 2) {
					Debug.Log ("FOUND TWO OR MORE OBJECTS IN ONE SQUARE");
                    // double for loop to find each event that could occur
                    for (int firstObjectIndex = 0; firstObjectIndex < objs.Count - 1; firstObjectIndex++)
                    {
                        for (int secondObjectIndex = firstObjectIndex + 1; secondObjectIndex < objs.Count; secondObjectIndex++)
                        {
                            BoardObject obj1 = objs[firstObjectIndex];
                            BoardObject obj2 = objs[secondObjectIndex];
                            List<BoardObject> objectPair = new List<BoardObject>();
                            objectPair.Add(obj1);
                            objectPair.Add(obj2);

                            BoardEvent e = new BoardEvent(new Vector2(i, j), objectPair);
							events.Add (e);
                        }
                    }
                }
            }
        }
        return events;
    }

	//takes any board object, places it at the positions it occupies within the map
	public void put(BoardObject obj){
        List<Vector2> positions = obj.getPositionAtTime(time);
//		Debug.Log ("OBJECT AT POSITION " + positions.Count);
	    foreach (Vector2 pos in positions)
        {
            map[Convert.ToInt32(pos.x), Convert.ToInt32(pos.y)].Add(obj);
        }
	}

	public List<BoardObject> get(Vector2 pos){
		List<BoardObject> objs = map[Convert.ToInt32(pos.x), Convert.ToInt32(pos.y)];
		return objs;
	}

	public bool isTraversable(Vector2 pos){
        List<BoardObject> objs = map[Convert.ToInt32(pos.x), Convert.ToInt32(pos.y)];
        
        // if any objects are not traversable at location return false
        if (objs.Exists(x => !x.traversable))
        {
            return false;
        }
        else
        {
            return true;
        }
	}

	//DRAW FUNCTIONS
	public int getWidth() {
		return width;
	}
	public int getHeight() {
		return height;
	}
	public List<BoardObject> getObjectAtPosition(Vector2 pos){
		return map [((int)pos.x), ((int)pos.y)];
	}

}
