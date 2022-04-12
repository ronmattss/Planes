using System;
using Ammo;
using Ammo.AmmunitionBehaviors;
using UnityEngine;
using Missile = Ammo.Missile;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class DebugMode : MonoBehaviour
    {
        public GameObject MissileObject;
        public GameObject MissileObject2;

        public Transform[] location;
        public GameObject player;
        private int _index = 0;
        public Missile missileProperties;
        public Missile missileProperties2;



        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SpawnMissile();
            }
        }

        void SpawnMissile()
        {
            /*if (location != null)
            {
                var loc = Random.Range(0, location.Length);
                Instantiate(MissileObject,location[_index].position , location[_index].rotation);
                _index++;
            }
            else
                Instantiate(MissileObject, randomPointOnScreen() , Quaternion.identity);

            if (_index == location.Length)
                _index = 0;*/

        }
    }
}

