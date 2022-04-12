using System;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs;
using UnityEngine;
using Utility;

namespace Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        // Future: Retrieve score from Leaderboards

        private int highScore;
        private int currentScore;

        private void Awake()
        {
            LoadHighScore();
        }

        public void SetHighScore(int score)
        {
            if (highScore < score)
            {
                highScore = score;
                SaveManager.Instance.SetHighScore(highScore);
                
            }
        }

        private void LoadHighScore()
        {
            highScore = SaveManager.Instance.GetHighScore();
        }

        public int GetHighScore()
        {
            return highScore;
        }

        public void SetCurrentScore(int score)
        {
            currentScore = score;
        }
        public int GetCurrentScore()
        {
            return currentScore;
        }

        public int AddTimeToScore()
        {
           var runScore = UIManager.Instance.GetDisplayScore() + UIManager.Instance.GetDisplayTime();
           if (runScore > highScore)
           {
               SetHighScore(runScore);
               UIManager.Instance.highScoreText.text = "HighScore: "+highScore;
           }
           return runScore;
        }

        public void CalculateCurrency()
        {
            var score = AddTimeToScore() /10;
            var currCurrency = SaveManager.Instance.GetCurrency();
           
            Debug.Log("Currency: "+score + "currCurrenct: "+ currCurrency);
            SaveManager.Instance.SetCurrency(score + currCurrency);
            Debug.Log("Currency2: "+ (score + currCurrency));
          //  UIManager.Instance.currencyText.text = "Stars: " + (score + currCurrency);
//            Debug.Log("SavedCurrency: "+ (SaveManager.Instance.GetCurrency()));


        }
        
    }
}