using System;
using System.Collections.Generic;
using DefaultNamespace.Store;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;


public class UIManager : Singleton<UIManager>
{

    public GameObject joyStick;
    public GameObject mainMenuUI;
    public GameObject retryUI;
    public GameObject gameUI;

    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text weaponText;
    public TMP_Text runScoreText;
    public TMP_Text highScoreText;
    public TMP_Text currencyText;

    public TMP_Text speedText;
    public TMP_Text handlingText;
    public TMP_Text purchaseButtonText;
    public TMP_Text DebugText;


    

    public GameObject panel;

    public Button button;
    public Button startButton;
    public Button purchaseButton;
    
   
    
    private int currentScore = 0;
    private int currentTime = 0;

    public void SetPropertyText(String speed, String handling)
    {
        speedText.text = "Speed: " + speed;
        handlingText.text = "Handling: " + handling;

    }

    private void Awake()
    {
        currencyText.text = "" + SaveManager.Instance.GetCurrency();
        highScoreText.text = "HighScore: " + SaveManager.Instance.GetHighScore();
  
        StoreManager.Instance.DebugPlanes();
    }

    public void DisplayCurrency()
    {
        currencyText.text = "" + SaveManager.Instance.GetCurrency();
        Debug.Log(currencyText.text);
    }


    public void RestartGame()
    {
        Debug.Log("is this invoking");
        button.gameObject.SetActive(false);
        panel.SetActive(false);
        GameplayManager.Instance.plane.SetActive(true);
        SpawnManager.Instance.enabled = true;
        joyStick.SetActive(true);
        currentScore = 0;
        currentTime = 0;
    }
    
    public void GiveFeedback()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdjtMxhfWrUT7SmngtEOoXahpD27pDlze9ffoC_jRGTZ685wQ/viewform?usp=pp_url");
        Debug.Log("Feedback");
    }

    public void ResetScores()
    {
        panel.SetActive(true);
        joyStick.SetActive(false);
        button.gameObject.SetActive(true);
        runScoreText.text = ScoreManager.Instance.AddTimeToScore().ToString();
        ScoreManager.Instance.CalculateCurrency();
        currencyText.text = "" + SaveManager.Instance.GetCurrency();
    }
    
    
    // Display Current Score
    private void DisplayScore()
    {
        scoreText.text = currentScore.ToString();
    }
    
    
    private void DisplayTime()
    {
        timeText.text = currentTime.ToString();
    }

    // Get Current Score and Time Survived

    public void SetScore(int currScore)
    {
        currentScore = currScore;
        DisplayScore();
    }
    public void SetTime(int currTime)
    {
        currentTime = currTime;
        DisplayTime();
    }
    public int GetDisplayScore()
    {
        return currentScore;
    }
    public int GetDisplayTime()
    {
        return currentTime;
    }
    

}




