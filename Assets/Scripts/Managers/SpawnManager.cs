using System.Collections.Generic;
using Ammo.AmmunitionBehaviors;
using DefaultNamespace.Pickups;
using UnityEngine;
using Utility;

namespace Managers
{
    // Script that handles Missiles and Pickup Spawning
    public class SpawnManager : Singleton<SpawnManager>
    {
        // Manages missiles
        public List<HomingMissile> missiles = new List<HomingMissile>();
        public List<OnPickUp> pickups = new List<OnPickUp>();

        public Spawner missileSpawner;
        public PickupSpawner pickupSpawner;
        public bool flag;
        public int secondsBeforeIncrementingMissileAllowed = 5;
        public int numberOfMissilesAllowed = 2;
        public int missileCount;
        public int incrementCount;
        public int spawnDelay = 3;
        private float _spawnDelayTimer;
        public float timer;
        private bool _incrementFlag;
        private int _swarmIncrement = 1;
        private int spawnChance = 50;
            

        private void OnDisable()
        {
           
            // Reenable Plane at 0,0
            // Show Restart UI Block
        }

        private void OnEnable()
        {
            _spawnDelayTimer = spawnDelay;
            timer = 0;
            incrementCount = 0;
            missileSpawner.SpawnMissileObject();
            _swarmIncrement = 1;
            UIManager.Instance.SetScore(0); // set Score to 0 
            
        }

        void Awake()
      {
          _spawnDelayTimer = spawnDelay;
          timer = 0;
          incrementCount = 0;
          
      }
        void FixedUpdate()
        {
            Spawn();
            Debug.Log(missiles.Count);
            if (missileCount < numberOfMissilesAllowed)
            {

                
            }

            if (missileCount <= 0)
            {
                missileCount = 1;
                missileSpawner.SpawnMissileFromList();
            }

            

        }



        public void UntargetMissiles()
        {
            foreach (var missile in missiles)
            {
                missile.missileTarget = missile.gameObject;
                missile.SelfDestruct();
            }
        }
        
        public void RemovePickups()
        {
            foreach (var pickup in pickups)
            {
                Destroy(pickup.gameObject);
            }
        }

        private void LateUpdate()
        {
            Counter();
        }

        void Spawn()
        {
            if (_spawnDelayTimer <= 0)
            {
                missileSpawner.SpawnMissileObject();
                /*var pickupChance = Random.Range(0, 100);
                if(spawnChance >= pickupChance) */
                    pickupSpawner.SpawnPickupsFromList();
                _spawnDelayTimer = spawnDelay;
                incrementCount++;
            }

            if (incrementCount == Random.Range(_swarmIncrement,_swarmIncrement+3))
            {
                incrementCount = 0;
                _swarmIncrement++;
                missileSpawner.SetSwarmSize(_swarmIncrement);
                missileSpawner.Swarm();

            }
            else
            {
                _spawnDelayTimer -= Time.deltaTime;
            }
        }

        void Counter()
        {
            timer += Time.deltaTime;
            var timeTemp = (int) timer;
            UIManager.Instance.SetTime(timeTemp);
            if (timeTemp == secondsBeforeIncrementingMissileAllowed)
            {

            }

            _incrementFlag = false;
        }
        
        


        
        

    }
}
