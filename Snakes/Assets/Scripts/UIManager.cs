using UnityEngine;
using System.Collections;
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

    private int currentLevel;
    private int snake;

    public List<Button> buttons;

	// Use this for initialization
	void Start () {
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
            Debug.Log("enabled help");
            Debug.Log(helpText.enabled);
        }
        else if (helpText.enabled == true)
        {
            helpText.enabled = false;
            Debug.Log("disabled help");
            Debug.Log(helpText.enabled);
        }

    }
    public void SetColors(List<Color> buttonColors)
    {
        
        for (int i=0; i< buttons.Count;i++)
        {
            if (i < buttonColors.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].enabled = true;
                var buttonColor = buttons[i].colors;
                buttonColor.normalColor = buttonColors[i];
                buttons[i].colors = buttonColor;
                buttons[i].image.sprite = Resources.Load<Sprite>("square");
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
        for (int i=0; i< completed.Count;i++)
        {
            if (completed[i])
            {
                Sprite buttonSprite = Resources.Load<Sprite>("completed");
                buttons[i].image.sprite = buttonSprite;
            }
            else if (!completed[i])
            {
                Sprite buttonSprite = Resources.Load<Sprite>("square");
                buttons[i].image.sprite = buttonSprite;
            }
        }
    }
    public void DisableSnakeSelection()
    {
        snakeSelectionBlocker.SetActive(true);
    }
    public void EnableSnakeSelection()
    {
        snakeSelectionBlocker.SetActive(false);
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
        }

    }
    public void LoadNextLevel()
    {
       
        activeSnakeFeedback(0);
        ResetTiles();
        LoadLevel(currentLevel + 1);
        finishedLevelCanvas.enabled = false;
    }
    public void activeSnakeFeedback(int newSnake)
    {
        buttons[snake].GetComponent<Outline>().enabled = false;
        //remove old active snake feedback
        //enable current active snake feedback
        
        buttons[newSnake].GetComponent<Outline>().enabled = true;
        snake = newSnake;

    }
}
