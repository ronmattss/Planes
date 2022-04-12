using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs;
using DefaultNamespace.Store;
using UnityEngine;
using Utility;

namespace Managers
{
    // Manager that handles Store related Logic
    public class StoreManager : Singleton<StoreManager>
    {
        // Check if plane is unlocked from profile
        // if not unlocked then display it to the store
        public List<Purchasable> availablePlanes = new List<Purchasable>();
        public List<PurchasableData> planeList = new List<PurchasableData>();


        private void Awake()
        {
            Debug.Log("Is this called Store");
            AddNewPlane();
        }

        public void DebugPlanes()
        {
            planeList = SaveManager.Instance.GetPurchasables().GetlistOfPurchasedData();
            Debug.Log("Is this called Store");
            Debug.Log($"Planes Available: {availablePlanes[0].price} {availablePlanes[1].price}");
            Debug.Log($"Planes Available from Save: {planeList[1].name} {planeList[1].price}");
            foreach (var plane in planeList)
            {
                Debug.Log("Plane Names"+plane.name);
            }
        }

        public void BuyPlane()
        {
            var currentPlane = PlaneManager.Instance.currentPlane;
            var toBeBought = planeList.Find(n => n.name == currentPlane.name);
            var currentMoney = SaveManager.Instance.GetCurrency();
            if (currentMoney >= toBeBought.price)
            {
                currentMoney -= toBeBought.price;
                 planeList.Find(n => n.name == toBeBought.name).isUnlocked = true;
                 SaveManager.Instance.SetCurrency(currentMoney);
                 UpdateSave();
                 UIManager.Instance.currencyText.text = currentMoney.ToString();
                 UIManager.Instance.startButton.gameObject.SetActive(true);
                 UIManager.Instance.purchaseButton.gameObject.SetActive(false);
            }
            else
            {
                UIManager.Instance.DebugText.text = " you don't have enough Stars";
            }
        }

        public void AddNewPlane()
        {

            if (availablePlanes.Count == planeList.Count) return;
            bool flagMatch = false;
            for (int i = 0; i < availablePlanes.Count; i++)
            {
                for (int j = 0; j < planeList.Count; j++)
                {
                    if (availablePlanes[i].name == planeList[j].name)
                    {
                        flagMatch = true;
                        continue;
                    }
                }

                if (!flagMatch)
                {
                    planeList.Add( new PurchasableData()
                    {
                        name = availablePlanes[i].name,
                        price = availablePlanes[i].price,
                        isUnlocked = availablePlanes[i].isUnlocked,
                        realPrice = availablePlanes[i].realPrice,
                        isInApp = availablePlanes[i].isInApp
                    });
                }
            }
            var saveData =  SaveManager.Instance.GetPurchasables();
            saveData.SetlistOfPurchasedData(planeList);





        }

        void UpdateSave()
        {
            var saveData =  SaveManager.Instance.GetPurchasables();
            saveData.SetlistOfPurchasedData(planeList);
        }

        // buttons yey
    }
}