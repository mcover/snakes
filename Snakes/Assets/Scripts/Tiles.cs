using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Tiles: MonoBehaviour {
//	private int tileWidth = 10;
//	private int tileHeight = 10;
    //public Transform tileCanvas;
	// Use this for initialization - init with empty board here?
	void Start () {
		drawEmptyBoard (7,7);
	}

	// private List<GameObject> mapTiles;
		void drawEmptyBoard(int mapWidth, int mapHeight) {
		
			for (int i = 0; i < mapWidth; i++) {
				for (int j = 0; j < mapHeight; j++) {
					GameObject tile = new GameObject();
//                    tile.transform.parent = this.transform;
					tile.transform.parent = this.gameObject.transform;
                    //tileCanvas.gameObject.;
//				    GameObject newTile = this.gameObject.AddComponent<GameObject>("Tile");
					Image tileImage = tile.AddComponent<Image> ();
					
					RectTransform rt = tileImage.rectTransform;
					RectTransform panelRT = (RectTransform)this.gameObject.transform;

					float width = rt.rect.width;
					float height = rt.rect.height;
					
					float pWidth = panelRT.rect.width; // 400
					float pHeight = panelRT.rect.height; // 400
					float scaleRatio = (float)(pWidth/mapWidth)/width;
					tileImage.transform.localScale = new Vector3 (scaleRatio,scaleRatio,1);
					Debug.Log	("ratio");
					Debug.Log (scaleRatio);
					Debug.Log (pWidth/mapWidth);

				    Sprite tileSprite = Resources.Load<Sprite>("completed_square") as Sprite;
				    tileImage.sprite = tileSprite;
                
					// transform.rotation not necessary untill handling boardObjects
					Vector3 tilePos = new Vector3(i*width*scaleRatio,j*height*scaleRatio,0);
					Vector3 panelOffset = new Vector3(-pWidth/2,-pHeight/2,0);
					Vector3 tileOffset = new Vector3 (width*scaleRatio/2, -height*scaleRatio/2,0);
					
					tile.transform.localPosition = tilePos + panelOffset + tileOffset;
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


