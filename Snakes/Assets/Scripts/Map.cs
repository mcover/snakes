using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map {

    private int time; //time step map represents
    private List<BoardObject>[,] map; //2d array of arrays of boardObjects
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
        List<BoardEvent> events = new List<BoardEvent>();
        // double for loop --> for each location
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                List<BoardObject> objs = map[i, j];
                
                // if at least one event could occur
                if (objs.Count >= 2) {
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

}
