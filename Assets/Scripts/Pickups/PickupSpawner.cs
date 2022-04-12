using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace DefaultNamespace.Pickups
{
    public class PickupSpawner : MonoBehaviour
    {
        public List<Transform> spawnPoints = new List<Transform>();
        public List<GameObject> pickUpList = new List<GameObject>();
        
        
        
        public void SpawnPickupsFromList()
        {
            int randPickup = Random.Range(0, pickUpList.Count);
            int randomPosition = Random.Range(0, spawnPoints.Count);
        
            GameObject pickup =  Instantiate(pickUpList[randPickup], spawnPoints[randomPosition].position , Quaternion.identity);
            SpawnManager.Instance.pickups.Add(pickup.GetComponent<OnPickUp>());
        }
        

        
        
    }
}