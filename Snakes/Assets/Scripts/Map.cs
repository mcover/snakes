﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }



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
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                List<BoardObject> objs = map[i, j];
                if (objs.Count >= 2) {
                    BoardEvent e = new BoardEvent(new Vector2(i, j), objs);
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

	public bool isTraversable(Vector2 pos){
        List<BoardObject> objs = map[Convert.ToInt32(pos.x), Convert.ToInt32(pos.y)];
        
        // if any objects are not traversable at location return false
        if (objs.Find(x => !x.traversable))
        {
            return false;
        }
        else
        {
            return true;
        }
	}

}