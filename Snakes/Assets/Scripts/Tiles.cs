﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Tiles: MonoBehaviour {
	// store the GameObjects in a List
    [HideInInspector]
	public GameObject[,] tileList;
    [HideInInspector]
    public GameObject[,] snakeList;

    public float pastSnakeAlpha = .5f;
    //public Transform tileCanvas;
	void Start () {
	}

	// private List<GameObject> mapTiles;
		public void drawEmptyBoard(int mapWidth, int mapHeight) {
		//Debug.Log ("Draw empty board being called");
		tileList = new GameObject[mapWidth, mapHeight];
        snakeList = new GameObject[mapWidth, mapHeight];
		//Debug.Log ("initialized tile list to " + tileList);
//        Debug.Log ("level dim" + mapWidth + " " + mapHeight);
			for (int i = 0; i < mapWidth; i++) {
				for (int j = 0; j < mapHeight; j++) {
					GameObject tile = new GameObject();
                    tile.name = "Tile";
                    
                    GameObject snakeTile = new GameObject();
                    snakeTile.name = "SnakeTile";
                //                    tile.transform.parent = this.transform;
                    tile.transform.parent = this.gameObject.transform;
                    snakeTile.transform.parent = this.gameObject.transform;
//				    GameObject newTile = this.gameObject.AddComponent<GameObject>("Tile");
					Image tileImage = tile.AddComponent<Image> ();
                    Image snakeSquare = snakeTile.AddComponent<Image>();
                    
					RectTransform rt = tileImage.rectTransform;
                    RectTransform st = snakeSquare.rectTransform;
					RectTransform panelRT = (RectTransform)this.gameObject.transform;

					float width = rt.rect.width;
					float height = rt.rect.height;
					
					float pWidth = panelRT.rect.width; // 400
					float pHeight = panelRT.rect.height; // 400

					float scaleRatio = (float)(pWidth/mapWidth)/width;

					tile.transform.localScale = new Vector3 (scaleRatio,scaleRatio,1);
                    snakeSquare.transform.localScale = new Vector3(scaleRatio, scaleRatio, 1);

				    Sprite tileSprite = Resources.Load<Sprite>("tile") as Sprite;
                    snakeSquare.enabled = false;
				    tileImage.sprite = tileSprite;
                
					// transform.rotation not necessary untill handling boardObjects
					Vector3 tilePos = new Vector3(i*width*scaleRatio,j*height*scaleRatio,0f);
					Vector3 panelOffset = new Vector3(-pWidth/2f,-pHeight/2f,0f);
					Vector3 tileOffset = new Vector3 (width*scaleRatio/2f, height*scaleRatio/2f,0f);
                    Vector3 snakeOffset = new Vector3(0f, 0f, 10);
					tile.transform.localPosition = tilePos + panelOffset + tileOffset;
                    snakeTile.transform.localPosition = tilePos + panelOffset + tileOffset+snakeOffset;
               
				//	store tile GameObjects to access later for updates

					tileList[i,j] = tile;
                    snakeList[i, j] = snakeTile;
				}
			}
		}
    
    // paint all tiles back to white		
    public void clearBoard() {
        foreach(GameObject tile in tileList) {
            tile.GetComponent<Image>().color = Color.white;
        }
        foreach (GameObject snake in snakeList)
        {
            snake.GetComponent<Image>().enabled = false;
        }
    }
	// I would personally add all the sprites you want as components to your GameObject and 
	// activate/deactivate the sprites as necessary.
		void drawUpdates() {
			
		}
		
	// or drawSnakes? allSnakes? pastSnakes?

//		public void drawSnakes(int gameTime, Snake activeSnake, List<Snake> pastSnakes) {
//			// drawing active snake
//			List<Vector2> activePositionList = activeSnake.getPositionAtTime(gameTime);
//			foreach (Vector2 pos in activePositionList) {
//				GameObject tile = tileList[Convert.ToInt32(pos.x), Convert.ToInt32(pos.y)];
//
//			}
//		}

	public void drawMap(Map map, Snake activeSnake){
		clearBoard();
		//Debug.Log("drawMap called");
		for (int i = 0; i < map.getWidth(); i++) {
			for (int j = 0; j < map.getHeight(); j++) {
				Vector2 mapPos = new Vector2 (i, j);
				List<BoardObject> drawObjects = map.getObjectAtPosition (mapPos);
				foreach (BoardObject drawThis in drawObjects) {
					List<string> spriteInfo = drawThis.getSpriteInPositionAtTime(mapPos,map.getTime());
                    //sprite info is now a list -> [tileType,orientation]
                    //tileType can be: WALL, GOAL, HEAD, CORNER, STRAIGHT, TAIL
                    //orientation can be: UP, DOWN, LEFT, RIGHT
                    string tileType = spriteInfo[0];
                    string direction = spriteInfo[1];
                    if (tileType.Equals("WALL")||tileType.Equals("GOAL"))
                    {
                        GameObject bottomTile = tileList[i, j];
                        bottomTile.GetComponent<Image>().color = drawThis.getColor();
                    }
                    //Debug.Log("drawThis tileType: " + tileType + " direction: " + direction);
                    if (!(tileType.Equals("WALL")) && !(tileType.Equals("GOAL")))
                    { 
                        GameObject tile = snakeList[i, j];
                        //tile.GetComponent<Image> ().color = drawThis.getColor ();
                        Image snakeImage = tile.GetComponentInChildren<Image>(); //the snake image
                        //Debug.Log(snakeImage == null);
                        snakeImage.enabled = true;
                        if (drawThis.getColor() == activeSnake.getColor())
                        {
                            snakeImage.color = drawThis.getColor();  //set color of image
                        }
                        else
                        {
                            snakeImage.color = drawThis.getColor();
                            snakeImage.color = new Color(snakeImage.color.r, snakeImage.color.g, snakeImage.color.b, pastSnakeAlpha);
                        }
                        Sprite newSprite = Resources.Load<Sprite>(tileType.ToString()) as Sprite; //grabs head from resources folder...maintains orientation in folder
//						Debug.Log("sprite info direction" + spriteInfo[1]);
                        snakeImage.sprite = newSprite;
						if (tileType.Equals("HEAD") || tileType.Equals("STRAIGHT")
							||tileType.Equals("TAIL") || (tileType.Equals("CORNER")))
                        {	
							// clear previous rotation history
							snakeImage.transform.rotation = Quaternion.identity;
                            if (spriteInfo[1].Equals("UP"))
                            {
                                snakeImage.transform.Rotate(new Vector3(0, 0, 0f));
                            }
                            else if (spriteInfo[1].Equals("DOWN"))
                            {
                                snakeImage.transform.Rotate(new Vector3(0, 0, 180f));
                            }
                            else if (spriteInfo[1].Equals("LEFT"))
                            {
                                snakeImage.transform.Rotate(new Vector3(0, 0, 90f));
                            }
                            else if (spriteInfo[1].Equals("RIGHT"))
                            {
                                snakeImage.transform.Rotate(new Vector3(0, 0, 270f));
                            }
                        }    
                   }
				}
			}
		}
	}

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


