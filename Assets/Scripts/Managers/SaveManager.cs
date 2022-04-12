using System;
using UnityEngine;
using Utility;

namespace Managers
{
    public class SaveManager : Singleton<SaveManager>
    {

        private PlayerProfile profile;
        private SavePurchasable savePurchases;
        private void Awake()
        {
            profile = new PlayerProfile();
            savePurchases = new SavePurchasable();
            
           
        }

        public SavePurchasable GetPurchasables() => savePurchases;

        public void SetPurchasables(SavePurchasable purchasables)
        {
            savePurchases = purchasables;
            savePurchases.SaveToFile();
        }


        public int GetHighScore()
        {
            return profile.GetHighScore();
        }
        public int GetCurrency()
        {
            return profile.GetCurrency();
        }
        public void SetHighScore(int score)
        {
             profile.SaveHighScore(score);
             profile.SaveToJson();
        //     SerializeSaveObject();
        }
        public void SetCurrency(int currency)
        {
             profile.SaveCurrency(currency);
             profile.SaveToJson();
            // SerializeSaveObject();
        }
        

 
        
        
    }
}