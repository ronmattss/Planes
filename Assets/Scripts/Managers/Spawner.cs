using System.Collections;
using System.Collections.Generic;
using Ammo;
using Ammo.AmmunitionBehaviors;
using Managers;
using UnityEngine;
using Utility;

public class Spawner : MonoBehaviour
{
    public GameObject missile;
    public GameObject missile2;
    public GameObject rocket;

    public GameObject player;
    
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> missileList = new List<GameObject>();

    private int _swarmSize;

    public int GetSwarmSize()
    {
        return _swarmSize;
    }

    public void SetSwarmSize(int size)
    {
        _swarmSize = size;
    }


    // Basic Spawning
    public void SpawnMissileObject()
    {
        int rand = Random.Range(0, 2);
        int randomPosition = Random.Range(0, spawnPoints.Count);
        if (rand == 0)
        {
            GameObject launchedMissile =  Instantiate(missile, spawnPoints[randomPosition].position, Quaternion.identity);
            launchedMissile.GetComponent<HomingMissile>().missileTarget = player;
            SpawnManager.Instance.missiles.Add(launchedMissile.GetComponent<HomingMissile>());
        }
        else
        {
            GameObject launchedMissile =  Instantiate(missile2, spawnPoints[randomPosition].position , Quaternion.identity);
            launchedMissile.GetComponent<HomingMissile>().missileTarget = player;
            SpawnManager.Instance.missiles.Add(launchedMissile.GetComponent<HomingMissile>());
        //    MissileManager.Instance.missiles.Add(launchedMissile.GetComponent<HomingMissile>());

        }

        SpawnManager.Instance.missileCount++;
    }


    public void SpawnMissileFromList()
    {
        int randMissile = Random.Range(0, missileList.Count);
        int randomPosition = Random.Range(0, spawnPoints.Count);
        
        GameObject launchedMissile =  Instantiate(missileList[randMissile], spawnPoints[randomPosition].position , Quaternion.identity);
        launchedMissile.GetComponent<HomingMissile>().missileTarget = player;
        SpawnManager.Instance.missiles.Add(launchedMissile.GetComponent<HomingMissile>());
        SpawnManager.Instance.missileCount++;
        
    }



    public void Swarm()
    {
      
        int counter = 0;
        StartCoroutine(SwarmSpawn(counter));
    }

    public void ButtonTest()
    {
        Debug.Log("Is This working?");
    }

    public IEnumerator SwarmSpawn(int counter)
    {
       
        counter++;
        int randomSpawn = Random.Range(0, spawnPoints.Count);
        
        if ((counter <=_swarmSize) && counter < spawnPoints.Count)
        {  Debug.Log("Swarmer: " + counter);
            yield return new WaitForSeconds(0.15f);
 float angleOfTarget =  ProjectileComputation.GetAngleOfTarget(spawnPoints[randomSpawn].position,GameplayManager.Instance.planeLookAhead.transform.position) - 180;
            GameObject launchedMissile = Instantiate(rocket, spawnPoints[randomSpawn].position, Quaternion.Euler(0,0,angleOfTarget));
            launchedMissile.GetComponent<HomingMissile>().missileTarget = GameplayManager.Instance.planeLookAhead;
            
            SpawnManager.Instance.missiles.Add(launchedMissile.GetComponent<HomingMissile>());
            StartCoroutine(SwarmSpawn(counter));
            yield return null;
        }
        else
        {
            StopCoroutine("SwarmSpawn");
            yield return null;
        }


    }
    
    
}
