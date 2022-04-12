using System.Collections.Generic;
using Planes.AircraftBehavior;
using Planes.AircraftData;
using UnityEngine;
using System;
using System.Linq;
using DefaultNamespace.Store;
using Utility;

namespace Managers
{
    public class PlaneManager : Singleton<PlaneManager>
    {
        // prototype
        // Plane Select
        // On Release Shop with unlocks and locks
       [SerializeField] List<Aircraft> availablePlanes = new List<Aircraft>();
        private int _currentIndex = 0;
        private int debugCount = 0;
        public Aircraft currentPlane;

        void Awake()
        {
           
            var speed = availablePlanes[_currentIndex].aircraftStats.topSpeed;
            var hand = availablePlanes[_currentIndex].aircraftStats.turnRate;
            UIManager.Instance.SetPropertyText(speed.ToString(),hand.ToString());
            Debug.Log($"Current Plane{GetCurrentPlaneModel()}");
      //     CheckIfPlaneIsUnlocked(availablePlanes[0].aircraftStats.model);

        }
      public  void SelectPlane(int direction)
      {
          debugCount = availablePlanes.Count;
            _currentIndex += direction;
            if (_currentIndex >= availablePlanes.Count)
            {
                _currentIndex = 0;
            }

            else if (_currentIndex < 0)
            {
                _currentIndex = availablePlanes.Count - 1;
            }
           // Debug.Log($"Is plane Unlocked? {CheckIfPlaneIsUnlocked(availablePlanes[_currentIndex].aircraftStats.model)}");
           CheckIfPlaneIsUnlocked(availablePlanes[_currentIndex].aircraftStats.model);
             currentPlane = availablePlanes[_currentIndex];
            GameplayManager.Instance.plane.GetComponent<AircraftStatus>().SetAircraft(availablePlanes[_currentIndex]);
            var speed =availablePlanes[_currentIndex].aircraftStats.topSpeed;
            var hand = availablePlanes[_currentIndex].aircraftStats.turnRate;
            UIManager.Instance.SetPropertyText(speed.ToString(),hand.ToString());
            GameplayManager.Instance.plane.GetComponent<AircraftControl>().InitializeData();

      }

      public String GetCurrentPlaneModel()
      {
          return availablePlanes[_currentIndex].aircraftStats.model;
      }
      // Check if Plane is Unlocked
      void CheckIfPlaneIsUnlocked(String planeName)
      {
          var planeDetails = planeData(planeName);
//          Debug.Log($"{planeName} {planeDetails.name}  {planeDetails.price}");
          bool isUnlocked = planeDetails.isUnlocked;
          if (isUnlocked)
          {
              UIManager.Instance.startButton.gameObject.SetActive(true);
              UIManager.Instance.purchaseButton.gameObject.SetActive(false);
          }
          
          else
          {
              UIManager.Instance.startButton.gameObject.SetActive(false);
              UIManager.Instance.purchaseButton.gameObject.SetActive(true);
              UIManager.Instance.purchaseButtonText.text = planeDetails.price.ToString();
              Debug.Log($"plane name: {planeDetails.name}"+planeDetails.price);


          }
      }

      PurchasableData planeData(String name)
      {
          var list = StoreManager.Instance.planeList;
          
          for (int i = 0; i < list.Count; i++)
          {
              if (name == list[i].name)
              {
                  return list[i];
              }
          }
          return null;
      }
      
      
    }
}