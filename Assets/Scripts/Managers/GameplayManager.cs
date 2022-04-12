using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Store;
using Managers;
using UnityEngine;
using Utility;

public class GameplayManager : Singleton<GameplayManager>
{

    public Camera uiCamera;
    public RectTransform canvas;
    public GameObject plane;
    public GameObject planeLookAhead;
    


    public float secondsBeforeUIShows = 1f;




    // When Plane is Disabled
    // Disable Gameplay
    public void ResetGameplay()
    {

        StartCoroutine(ResetTimer());
    }


  

    IEnumerator ResetTimer()
    {
        yield return  new WaitForSeconds(secondsBeforeUIShows);
        SpawnManager.Instance.UntargetMissiles();
        SpawnManager.Instance.RemovePickups();
        SpawnManager.Instance.enabled = false;
        plane.transform.rotation = Quaternion.Euler(0,0,0);
        UIManager.Instance.ResetScores();


        yield return null;
        StopCoroutine(ResetTimer());
    }
}
