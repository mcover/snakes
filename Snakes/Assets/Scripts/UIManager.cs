using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Canvas startCanvas;
    public Canvas levelCanvas;
    public Canvas creditCanvas;
    public Canvas settingsCanvas;

	// Use this for initialization
	void Start () {
        startCanvas.enabled = true;
        levelCanvas.enabled = false;
        creditCanvas.enabled = false;
        settingsCanvas.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayGame()
    {
        //go to level screen
        Debug.Log("play game");
        levelCanvas.enabled = true;
        //startCanvas.enabled = false;
    }

    public void EnableCredits()
    {
        Debug.Log("enable credits");
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
        Debug.Log("enable settings");
        //enable settings panel and buttons
    }
    public void DisableSetting()
    {
        settingsCanvas.enabled = false;
        //disable settings panel and buttons
    }
    public void FromLevelToMain()
    {
        //disable level canvas and enable main canvas
        startCanvas.enabled = true;
        levelCanvas.enabled = false;
    }
    public void LoadLevel(int num)
    {
        //load level num
    }
}
