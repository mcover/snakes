﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tiles: MonoBehaviour {
	private int tileWidth = 100;
	private int tileHeight = 100;
	// Use this for initialization - init with empty board here?
	void Start () {
		drawEmptyBoard (7,7);
	}

	// private List<GameObject> mapTiles;
		void drawEmptyBoard(int mapWidth, int mapHeight) {
			for (int i = 0; i < mapWidth; i++) {
				for (int j = 0; j < mapHeight; j++) {
					GameObject tile = new GameObject();
					SpriteRenderer tileRenderer = tile.AddComponent<SpriteRenderer> ();
					tileRenderer.sprite = Resources.Load<Sprite>("Sprites/snake_face");
					// transform.rotation not necessary untill handling boardObjects
					Vector3 tilePos = new Vector3(i*tileWidth,j*tileHeight,0);
					tile.transform.position = tilePos;
				}
			}
		}


	// I would personally add all the sprites you want as components to your GameObject and 
	// activate/deactivate the sprites as necessary.

		void drawUpdates() {
		
		}
		
	// or drawSnakes? allSnakes? pastSnakes?
		void drawSnakes() {
		}

		// Tile object should read information from GameLoop
		// and render the correct type of tile in the scene. 
		// 2. render puzzleObjects tiles (update/draw snakes & walls)

		// Tile types: wall/obstacles, head, body, turn, tail, map
		// TODO: need map art
		// Tile position: 
		// initialize tiles with initPositions of the snakes, 
		// iterate through snakeList - passed in from GameLoop
		// for each snake: 
		// 		for each pos in List<Vector2> getPositionAtTime(int t), 
		// 			story[0] = tail
		//			story[len] = head
		//			story[middle] = read turnHistory to determine body or turn. 

}

