using System;
using System.Security.Cryptography;
using Managers;
using Planes.AircraftBehavior;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Random = System.Random;

namespace Ammo.AmmunitionBehaviors
{
  
    public class HomingMissile : MonoBehaviour
    {

        public Missile missile;
        public Rigidbody2D missileBody;
        public GameObject missileTarget;
        public GameObject particleSystem;
        public AudioSource missileSoundSource;
        public AudioClip missileAudioClip;
        public float missileTopSpeed;
        public float missileDamage;
        public float missileFollowTime;
        public float missileTurnRate;

        private bool _incrementFlag = false;
        
        private void OnEnable()
        {

            //DEBUG ONLY
           // missileTarget = GameObject.FindWithTag("Dummy");
           if (missile != null)
           {
               SetupMissile(missile);
           }
            missileBody = GetComponent<Rigidbody2D>();
            
           
            
            

        }

        private void SetupMissile(Missile missileProperty)
        {
            missileTopSpeed = missileProperty.missileTopSpeed;
            missileFollowTime = missileProperty.missileSeekTime;
            missileDamage = missileProperty.damage;
            missileTurnRate = missileProperty.missileMaxTurnRate;
            missileAudioClip = missileProperty.missileLaunchSound;
            
           
            
        }

        public void PlayLaunchSound()
        {
            missileSoundSource.clip = missileAudioClip;
            missileSoundSource.Play();
        }
        private void FixedUpdate()
        {

            if (GetComponent<Renderer>().isVisible)
            {
                GetComponent<Target>().enabled = false;
            }
            else
            {
                GetComponent<Target>().enabled = true;
            }
            missileBody.AddForce(transform.up * missileTopSpeed);
            missileFollowTime -= Time.deltaTime;
          if (missileFollowTime <= 0)
          {
              Destroy(this.gameObject,3);
          }
          else
          {
              physicsTurn();
              
          }
        }


       


        public void physicsTurn()
        {
            
            Vector2 direction = transform.position - missileTarget.transform.position;
            direction.Normalize();
            
     
            //get the angle between transform.forward and target delta
            float angleDiff = Vector3.Angle(transform.forward, direction);

            float cross = Vector3.Cross(direction, transform.up).z;
            
            Debug.Log($"Missile TUrning Information: {angleDiff} {cross} {(cross * angleDiff * missileTopSpeed )} With TurnRate {((cross * angleDiff * missileTopSpeed * missileTurnRate))} ");
            missileBody.AddTorque(cross * angleDiff * missileTopSpeed); // drag?


        }



        private void OnDestroy()
        {

            particleSystem.GetComponent<DespawnParticleSystem>().enabled = true;
            particleSystem.transform.parent = null;
            
            SpawnManager.Instance.missiles.Remove(this);
            SpawnManager.Instance.missiles.Capacity--;
            if(!_incrementFlag)
             IncrementScore();
            _incrementFlag = false;
        }

        public void SelfDestruct()
        {
            
            Destroy(this.gameObject,.25f);
        }




        public void IncrementScore()
        {
            if (!GameplayManager.Instance.plane.activeSelf) return;
            int currScore =  UIManager.Instance.GetDisplayScore();
            currScore++;
            UIManager.Instance.SetScore(currScore);
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {

            SpawnManager.Instance.missileCount--;
            Debug.Log(other.tag);
            if (other.transform.CompareTag("Missile"))
            {
                
                Destroy(this.gameObject);
            }
            if (missileTarget == null) return;
            if (other.gameObject.name.Equals(GameplayManager.Instance.plane.transform.name))
            {
                _incrementFlag = true;
                other.gameObject.GetComponent<AircraftStatus>().SetHealth(missileDamage);
                particleSystem.transform.parent = null;
                particleSystem.GetComponent<AudioSource>().enabled = true;
                particleSystem.GetComponent<ParticleSystem>().Stop();
                Destroy(this.gameObject);
            }
        }
    }
}