using System;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Managers;
using Planes.AircraftBehavior;
using UnityEngine;

namespace Utility
{
    enum Data
    {
        Currency,
        HighScore
    }
    [Serializable]
    public class PlayerProfile
    {
        // Class representation of a Saved File
        // Local Saving
        // Gonna be changed to a JSON File
        // Store bought Items should also be Saved

        public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Data/";
        private int currency;
        private int highScore;
        private List<int> planeHighScore = new List<int>(); 
        private List<String> availablePlanes = new List<String>(); // list of purchased planes


        public PlayerProfile()
        {
       //     UIManager.Instance.DebugText.text = Directory.Exists(SAVE_FOLDER).ToString();
           LoadFromJson();
        }

        public String GetPlaneScore()
        {
            var planeModel = PlaneManager.Instance.GetCurrentPlaneModel();
            return "hello";
        }
        

        public void LoadFromJson()
        {
            // Check if directory exists
            if (!Directory.Exists(SAVE_FOLDER))
            {
                Debug.Log("Directory Does Not Exist");
                Directory.CreateDirectory(SAVE_FOLDER);
                currency = 0;
                highScore = 0;
                SaveToJson(); // initial file to populate save
            }
            
            if (!File.Exists(SAVE_FOLDER + "save.txt"))
            {
                Debug.Log("File Does Not Exist");
                currency = 0;
                highScore = 0;
                SaveToJson();
            }

           
            if (!File.Exists(SAVE_FOLDER + "save.txt")) return;
            string path = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            SaveProfile loadedProfile = JsonUtility.FromJson<SaveProfile>(path);
            currency = loadedProfile.Currency;
            highScore = loadedProfile.HighScore;
            Debug.Log("Loaded from JSON");
        }

        public void SaveToJson()
        {
            SaveProfile profile = new SaveProfile{Currency = currency,HighScore = highScore};
            File.WriteAllText(SAVE_FOLDER + "/save.txt",JsonUtility.ToJson(profile));
        }

        // Saves Highscore
        public void SaveHighScore(int score)
        {
            highScore = score;
            SaveToJson();
            Debug.Log("Saved: "+ score);
        }
        public void SaveCurrency(int curr)
        {
            currency = curr;
            Debug.Log("Saved Currency: "+ curr);
            SaveToJson();
        }

        public int GetCurrency() => currency;
        public int GetHighScore() => highScore;

        
        
        // Debug
        public void SerializeSaveObject()
        {
            SaveProfile profile = new SaveProfile{Currency = currency,HighScore = highScore};
            string json = JsonUtility.ToJson(profile);
            
            Debug.Log(json);
            File.WriteAllText(Application.dataPath + "/save.txt",json);
            LoadProfileFromJson();
        }

        public void LoadProfileFromJson()
        {
            string path = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            SaveProfile loadedProfile = JsonUtility.FromJson<SaveProfile>(path);
            Debug.Log($"Data from JSON: {loadedProfile.Currency}");
        }
        
    }
}