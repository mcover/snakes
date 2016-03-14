using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Tiles: MonoBehaviour {
	// store the GameObjects in a List
	public GameObject[,] tileList;
    //public Transform tileCanvas;
	void Start () {
	}

	// private List<GameObject> mapTiles;
		public void drawEmptyBoard(int mapWidth, int mapHeight) {
		Debug.Log ("Draw empty board being called");
		tileList = new GameObject[mapWidth, mapHeight];
		Debug.Log ("initialized tile list to " + tileList);
//        Debug.Log ("level dim" + mapWidth + " " + mapHeight);
			for (int i = 0; i < mapWidth; i++) {
				for (int j = 0; j < mapHeight; j++) {
					GameObject tile = new GameObject();
                    tile.name = "Tile";
                    
//                    tile.transform.parent = this.transform;
					tile.transform.parent = this.gameObject.transform;
//				    GameObject newTile = this.gameObject.AddComponent<GameObject>("Tile");
					Image tileImage = tile.AddComponent<Image> ();
                    GameObject snakeImage = (GameObject) Instantiate(Resources.Load("snakeImage"),tile.transform.position, Quaternion.identity);
                    snakeImage.transform.SetParent(tile.transform);
                    snakeImage.SetActive(false);
					RectTransform rt = tileImage.rectTransform;
					RectTransform panelRT = (RectTransform)this.gameObject.transform;

					float width = rt.rect.width;
					float height = rt.rect.height;
					
					float pWidth = panelRT.rect.width; // 400
					float pHeight = panelRT.rect.height; // 400

					float scaleRatio = (float)(pWidth/mapWidth)/width;

					tile.transform.localScale = new Vector3 (scaleRatio,scaleRatio,1);
//					Debug.Log	("ratio");
//					Debug.Log (scaleRatio);
//					Debug.Log (pWidth/mapWidth);
				    Sprite tileSprite = Resources.Load<Sprite>("tile") as Sprite;
				    tileImage.sprite = tileSprite;
                
					// transform.rotation not necessary untill handling boardObjects
					Vector3 tilePos = new Vector3(i*width*scaleRatio,j*height*scaleRatio,0f);
					Vector3 panelOffset = new Vector3(-pWidth/2f,-pHeight/2f,0f);
					Vector3 tileOffset = new Vector3 (width*scaleRatio/2f, height*scaleRatio/2f,0f);
					
					tile.transform.localPosition = tilePos + panelOffset + tileOffset;
				//	store tile GameObjects to access later for updates

					tileList[i,j] = tile;
				}
			}
		}
    
    // paint all tiles back to white		
    public void clearBoard() {
        foreach(GameObject tile in tileList) {
            tile.GetComponent<Image>().color = Color.white;
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

	public void drawMap(Map map){
		clearBoard();
		for (int i = 0; i < map.getWidth(); i++) {
			for (int j = 0; j < map.getHeight(); j++) {
				List<BoardObject> drawObjects = map.getObjectAtPosition (new Vector2 (i, j));
				foreach (BoardObject drawThis in drawObjects) {
					List<string> spriteInfo = drawThis.getSpriteInPositionAtTime(new Vector2 (i, j),map.getTime());
                    //sprite info is now a list -> [tileType,orientation]
                    //tileType can be: WALL, GOAL, HEAD, CORNER, STRAIGHT, TAIL
                    //orientation can be: UP, DOWN, LEFT, RIGHT
                    string tileType = spriteInfo[0];
                    string direction = spriteInfo[1];

                    if (!(tileType.Equals("WALL")) || !(tileType.Equals("GOAL")))
                    {
                        GameObject tile = tileList[i, j];
                        //tile.GetComponent<Image> ().color = drawThis.getColor ();
                        Image snakeImage = tile.GetComponentInChildren<Image>(); //the snake image
                        //Debug.Log(snakeImage == null);
                        snakeImage.enabled = true;
                        snakeImage.color = drawThis.getColor();  //set color of image
                        Sprite newSprite = Resources.Load<Sprite>(tileType.ToString()) as Sprite; //grabs head from resources folder...maintains orientation in folder
                        Debug.Log(spriteInfo[1]);
                        snakeImage.sprite = newSprite;
                        if (tileType.Equals("HEAD") || tileType.Equals("STRAIGHT")||tileType.Equals("TAIL"))
                        {
                            if (spriteInfo[1].Equals("UP"))
                            {
                                snakeImage.transform.Rotate(new Vector3(0, 0, 0));
                            }
                            else if (spriteInfo[1].Equals("DOWN"))
                            {
                                snakeImage.transform.Rotate(new Vector3(0, 0, 180));
                            }
                            else if (spriteInfo[1].Equals("LEFT"))
                            {

                                snakeImage.transform.Rotate(new Vector3(0, 0, 270));
                            }
                            else if (spriteInfo[1].Equals("RIGHT"))
                            {
                                snakeImage.transform.Rotate(new Vector3(0, 0, 90));
                            }
                                
                        }
                         
                        //snakeImage.transform.Rotate();
                        //
                   }
				}
			}
		}
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


