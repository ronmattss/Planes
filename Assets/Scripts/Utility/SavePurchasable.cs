using System;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using DefaultNamespace.Store;
using Managers;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public class SavePurchasable
    {
        public  readonly string SAVE_FOLDER = PlayerProfile.SAVE_FOLDER;

        private List<PurchasableData> purchasedData = new List<PurchasableData>();

        public SavePurchasable()
        {
            LoadFromFile();
            Debug.Log("Purchasable Loaded");
        }

        public List<PurchasableData> GetlistOfPurchasedData() => purchasedData;

        public void SetlistOfPurchasedData(List<PurchasableData> data)
        {
            purchasedData = data;
            SaveToFile();
        }

        public void LoadFromFile()
        {
            // Check if directory exists
            if (!Directory.Exists(SAVE_FOLDER))
            {
                InitializeData(StoreManager.Instance.availablePlanes);
                Directory.CreateDirectory(SAVE_FOLDER);
                SaveToFile(); // initial file to populate save
            }

            if (!File.Exists(SAVE_FOLDER + "purchasedData.txt"))
            {
                InitializeData(StoreManager.Instance.availablePlanes);
                SaveToFile();
            }
            string path = File.ReadAllText(SAVE_FOLDER + "/purchasedData.txt");
            PurchasedData loadedData = JsonUtility.FromJson<PurchasedData>(path);
            purchasedData = loadedData.purchasableData;
            Debug.Log($"Loaded from JSON {purchasedData[0]} \n {purchasedData[1]}");
            Debug.Log("Planes Loaded");
        }

        public void SaveToFile()
        {
            PurchasedData profile = new PurchasedData{purchasableData = purchasedData};
            var toSave = JsonUtility.ToJson(profile);
            File.WriteAllText(SAVE_FOLDER + "/purchasedData.txt",toSave);
            
        }

        void InitializeData(List<Purchasable> aircrafts) // will be called once
        {
            for (int i = 0; i < aircrafts.Count; i++)
            {
                var temp = new PurchasableData
                {   name = aircrafts[i].aircraft.name, 
                    price = aircrafts[i].price,
                    isUnlocked =  aircrafts[i].isUnlocked,
                    isInApp = aircrafts[i].isInApp,
                    realPrice = aircrafts[i].realPrice
                };
                purchasedData.Add(temp);
                Debug.Log(purchasedData[i]);
            }
        }
        
        
        
    }
}