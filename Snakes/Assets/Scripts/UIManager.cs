using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    public Canvas startCanvas;
    public Canvas levelCanvas;
    public Canvas creditCanvas;
    public Canvas settingsCanvas;
    public Canvas mainCanvas;
    public Text helpText;
    public Canvas finishedLevelCanvas;
    public GameObject snakeSelectionBlocker;
    public Transform boardPanel;
    public int maxLevel = 8;
    public Canvas collisionCanvas;
    public float delayTime = 0.5f;
    public Text selectASnake;

    private int currentLevel;
    private int snake;

    public List<Button> buttons;

	// Use this for initialization
	void Start () {
        selectASnake.enabled = true;
        collisionCanvas.enabled = false;
        snakeSelectionBlocker.SetActive(false);
        startCanvas.enabled = true;
        levelCanvas.enabled = false;
        creditCanvas.enabled = false;
        settingsCanvas.enabled = false;
        mainCanvas.enabled = false;
        helpText.enabled = false;
        finishedLevelCanvas.enabled = false;
        foreach (Button snakeButton in buttons)
        {
            snakeButton.GetComponent<Outline>().enabled = false;
            snakeButton.enabled = false;
        }
        snake = 0;
        activeSnakeFeedback(0);
	
	}
	

    public void PlayGame()
    {
        //go to level screen
        //Debug.Log("play game");
        levelCanvas.enabled = true;
        //startCanvas.enabled = false;
    }

    public void EnableCredits()
    {
        //Debug.Log("enable credits");
        creditCanvas.enabled = true;
        //enable credits panel and button
    }
    public void DisableCredits()
    {
        creditCanvas.enabled = false;
        //disable credits panel and button
    }
    public void EnableSettings()
    {
        settingsCanvas.enabled = true;
        //Debug.Log("enable settings");
        //enable settings panel and buttons
    }
    public void DisableSetting()
    {
        settingsCanvas.enabled = false;
        //disable settings panel and buttons
    }
    public void FromLevelToStart()
    {
        //disable level canvas and enable start canvas
        startCanvas.enabled = true;
        levelCanvas.enabled = false;
    }
    public void LoadLevel(int num)
    {
        //load level num
        levelCanvas.enabled = false;
        mainCanvas.enabled = true;
        currentLevel = num;
        
        activeSnakeFeedback(0);

        this.GetComponent<GameLoop>().loadLevel(num);
    }
    public void BackToMenu()
    {
        mainCanvas.enabled = false;
        startCanvas.enabled = true;
        levelCanvas.enabled = false;
        finishedLevelCanvas.enabled = false;
        ResetTiles();
    }
    public void ToggleHelpText()
    {
        if (helpText.enabled == false)
        {
            helpText.enabled = true;
           
        }
        else if (helpText.enabled == true)
        {
            helpText.enabled = false;
           
        }

    }
    public void SetColorsAndLengths(List<Color> buttonColors,List<int> lengths)
    {
        
        for (int i=0; i< buttons.Count;i++)
        {
            if (i < buttonColors.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].enabled = true;
                buttons[i].GetComponentInChildren<Text>().enabled = true;
                buttons[i].GetComponentInChildren<Text>().text= lengths[i].ToString();
                var buttonColor = buttons[i].colors;
                buttonColor.normalColor = buttonColors[i];
                buttons[i].colors = buttonColor;
                buttons[i].image.sprite = Resources.Load<Sprite>("square");
                buttons[i].image.color = buttonColors[i];
                buttons[i].GetComponent<Outline>().effectColor = buttonColors[i];
            }
            else if (i>=buttonColors.Count)
            {
                buttons[i].enabled = false; //makes unused buttons go away?
                buttons[i].gameObject.SetActive(false);
                //Debug.Log("disabling buttons");
            }
        }
    }
    public void UpdateSnakeButtons(List<bool> completed)
    {
        if (completed.Count == 0)
        {
            foreach (Button button in buttons)
            {
                Sprite buttonSprite = Resources.Load<Sprite>("square");
                button.image.sprite = buttonSprite;
                button.GetComponentInChildren<Text>().enabled = true;
            }
        }
        for (int i=0; i< completed.Count;i++)
        {
            if (completed[i])
            {
                Sprite buttonSprite = Resources.Load<Sprite>("completed");
                buttons[i].image.sprite = buttonSprite;
                buttons[i].GetComponentInChildren<Text>().enabled = false;
            }
            else
            {
                Sprite buttonSprite = Resources.Load<Sprite>("square");
                buttons[i].image.sprite = buttonSprite;
                buttons[i].GetComponentInChildren<Text>().enabled = true;
                //Debug.Log("RETURNING TO ORIG SPRITE!!!!");
            }
        }
    }
    public void DisableSnakeSelection()
    {
        snakeSelectionBlocker.SetActive(false);
        selectASnake.enabled = true;
    }
    public void EnableSnakeSelection()
    {
        selectASnake.enabled = false;
        snakeSelectionBlocker.SetActive(true);
    }
    public void ResetTiles()
    {
        int childs = boardPanel.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(boardPanel.transform.GetChild(i).gameObject);
        }
        
    }
    public void WonLevel()
    {
        finishedLevelCanvas.enabled = true;
        foreach (Button b in buttons)
        {
            b.image.sprite = Resources.Load<Sprite>("completed");
            b.GetComponentInChildren<Text>().enabled = false;
        }

    }
    public void LoadNextLevel()
    {
        if (currentLevel < maxLevel)
        {
            activeSnakeFeedback(0);
            ResetTiles();
            LoadLevel(currentLevel + 1);
            finishedLevelCanvas.enabled = false;
        }
        else
        {
            BackToMenu();
            EnableCredits();
        }
    }
    public void activeSnakeFeedback(int newSnake)
    {
        buttons[snake].GetComponent<Outline>().enabled = false;
        //remove old active snake feedback
        //enable current active snake feedback
        
        buttons[newSnake].GetComponent<Outline>().enabled = true;
        snake = newSnake;

    }

    public void ResetLevel()
    {
        ResetTiles();
        LoadLevel(currentLevel);
    }
	public void OnCollision(BoardObject aggressor, BoardObject victim)
    {
        collisionCanvas.enabled = true;
        Text[] collisionInfo =collisionCanvas.GetComponentsInChildren<Text>();
		Dictionary<string, Color> colorDict = this.GetComponent<GameLoop> ().color_map; 
		Color aggressorColor = aggressor.getColor ();
		Color victimColor = victim.getColor ();
		string aggressorColorName = colorDict.FirstOrDefault(x => x.Value == aggressorColor).Key;
		string victimColorName = colorDict.FirstOrDefault(x => x.Value == victimColor).Key;
		collisionInfo[1].text = aggressorColorName.ToString() + " snake" + " hit " + victimColorName.ToString() + "snake!";
		StartCoroutine(DelayToDisable(collisionCanvas));
    }

    IEnumerator DelayToDisable(Canvas c)
    {
        yield return new WaitForSeconds(delayTime);
        c.enabled = false;
    }
}
