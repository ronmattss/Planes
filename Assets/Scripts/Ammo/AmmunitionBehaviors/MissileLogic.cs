using System;
using Planes.AircraftBehavior;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Random = System.Random;

namespace Ammo.AmmunitionBehaviors
{
  
    public class MissileLogic : MonoBehaviour, IPropelProjectile,ISteerProjectile,ISpeedControl
    {
        
        public Rigidbody2D missileBody;
        public GameObject missileTarget;
        public GameObject particleSystem;
        public AudioSource missileSoundSource;
        public AudioClip missileAudioClip;
        public float missileMaxRange;
        public float missileTopSpeed;
        public float missileMaxTurnRate;
        public float missileDamage;
        public float missileMinSpeed;
        public float missileCurrentSpeed;
        public float missileAcceleration;
        public float missileCurrentTurnRate;
        public float missileMinimumTurnRate;
        public float missileFollowTime;
        public bool basicTurning = false;

        
        private void OnEnable()
        {

            missileCurrentTurnRate = 1;
            //DEBUG ONLY
           // missileTarget = GameObject.FindWithTag("Dummy");
            missileBody = GetComponent<Rigidbody2D>();
           
            
            

        }

        public void SetupMissile(GameObject target,float maxRange,float topSpeed,float acceleration,float maxTurnRate,float minTurnRate,float followTime,float damage,AudioClip clip)
        {
            missileMaxRange = maxRange;
            missileTopSpeed = topSpeed;
            missileAcceleration = acceleration;
            missileMaxTurnRate = maxTurnRate;
            missileMinimumTurnRate = minTurnRate;
            missileFollowTime = followTime;

            missileCurrentSpeed = missileTopSpeed * 0.75f;
            missileMinSpeed = missileTopSpeed * 0.25f;
            missileMaxTurnRate = maxTurnRate;
            missileCurrentTurnRate = 1;
            missileMinimumTurnRate = minTurnRate;
            missileDamage = damage;
            missileTarget = target;
            missileAudioClip = clip;
            missileSoundSource.Play();
        }
        public void SetupMissile(float maxRange,float topSpeed,float acceleration,float maxTurnRate,float minTurnRate,float followTime,float damage,AudioClip clip)
        {
            missileMaxRange = maxRange;
            missileTopSpeed = topSpeed;
            missileAcceleration = acceleration;
            missileMaxTurnRate = maxTurnRate;
            missileMinimumTurnRate = minTurnRate;
            missileFollowTime = followTime;

            missileCurrentSpeed = missileTopSpeed * 0.75f;
            missileMinSpeed = missileTopSpeed * 0.25f;
            missileMaxTurnRate = maxTurnRate;
            missileCurrentTurnRate = 1;
            missileMinimumTurnRate = minTurnRate;
            missileDamage = damage;
            missileAudioClip = clip;
            
        }

        public void PlayLaunchSound()
        {
            missileSoundSource.clip = missileAudioClip;
            missileSoundSource.Play();
        }
        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            if (missileMaxRange >= 0)
            {
                var missileTransform = transform;

                missileBody.velocity = missileTransform.up * missileCurrentSpeed;
               // DebugAngles();
                
                CalculatedTurn();
                // Turn(direction);

            }
            else
            {
                Destroy(gameObject);
            }
            missileMaxRange -= Time.deltaTime;
        }
        void CalculatedTurn()
        {
            if (missileTarget == null)
            {
                Accelerate(); // just accelerate
                return;
            }
            var missileTransform = transform;
            var targetAngle =  
                ProjectileComputation.GetAngleOfTarget(missileTransform.position, missileTarget.transform.position) + 180;
            if (missileFollowTime > 0)
            {
                if (!basicTurning)
                {
                    Turn(targetAngle);
                }
                else
                {
                    TurnBasic(targetAngle);
                }
            }
        }
        public void Turn(float direction)
        {
            var previousAngle =(int) transform.rotation.eulerAngles.z;
            Quaternion q = Quaternion.AngleAxis(direction, transform.forward);
            
            
            var rateOfTurn = ProjectileComputation.TurnRate(1,missileMaxTurnRate,missileCurrentSpeed);
           // transform.Rotate(0,0,rateOfTurn);
            this.transform.rotation = Quaternion.SlerpUnclamped(transform.rotation,q,rateOfTurn *Time.deltaTime);

            var currentAngle =(int) transform.rotation.eulerAngles.z;
           // Debug.Log($"previousAngle:{previousAngle} currentAngle: {currentAngle}");
            if (previousAngle == currentAngle)
            {
                Accelerate();
            }
            else
            {
                Decelerate();
                missileFollowTime -= Time.deltaTime;
            }

        }
        public void TurnBasic(float direction)
        {
            var previousAngle =(int) transform.rotation.eulerAngles.z;
            Quaternion q = Quaternion.AngleAxis(direction, transform.forward);
            
            
            var rateOfTurn = ProjectileComputation.TurnRate(1,missileMaxTurnRate,missileCurrentSpeed);
            // transform.Rotate(0,0,rateOfTurn);
            this.transform.rotation = Quaternion.SlerpUnclamped(transform.rotation,q,rateOfTurn *Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(direction, Vector3.forward),
                missileMaxTurnRate * Time.deltaTime);
            var currentAngle =(int) transform.rotation.eulerAngles.z;
            Accelerate();
            if (previousAngle != currentAngle)
            {
                missileFollowTime -= Time.deltaTime;

            }
            else
            {
            }

        }
        
        public void DebugAngles()
        {
            var missileTransform = transform;
            var targetAngle =  
                ProjectileComputation.GetAngleOfTarget(missileTransform.position, missileTarget.transform.position);
            var missileCurrentAngle = missileTransform.rotation.eulerAngles.z; // add 180 if not using Euler
            var computedAngle = missileCurrentAngle + targetAngle;
            var testComputedEulerAngle = (missileCurrentAngle - targetAngle - 180) *-1;
            var testPredictedCurrentAngle = missileCurrentAngle + testComputedEulerAngle;
           // Debug.Log($"CurrentAngle in Euler Angles: {missileCurrentAngle} rotateToThisAngle: {targetAngle} ComputedAngle{testPredictedCurrentAngle} ComputedEulerAngle: {testComputedEulerAngle}");
           // Debug.Log("Missile Predicted Angle: "+computedAngle + "current Angle: "+missileTransform.rotation.eulerAngles.z +" targetAngle: "+targetAngle + " Direction: "+direction);
        }

  

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (missileTarget == null) return;
            if (other.gameObject.name.Equals(missileTarget.transform.name))
            {
                other.gameObject.GetComponent<AircraftStatus>().SetHealth(missileDamage);
                particleSystem.transform.parent = null;
                Destroy(particleSystem,3);
                particleSystem.GetComponent<AudioSource>().enabled = true;
                particleSystem.GetComponent<ParticleSystem>().Stop();

                Destroy(gameObject);

            }
        }

        public void Accelerate()
        {
            if (missileCurrentSpeed <= missileTopSpeed)
            {
                missileCurrentSpeed += missileAcceleration * Time.deltaTime;
                missileCurrentSpeed += missileAcceleration * Time.deltaTime;
            }
            if (missileCurrentTurnRate >= missileMinimumTurnRate)
            {
                missileCurrentTurnRate -= 0.025f * Time.deltaTime;
            }
        }

        public void Decelerate()
        {
            if (missileCurrentSpeed >= missileMinSpeed)
            {
                missileCurrentSpeed -= missileAcceleration * Time.deltaTime;
                
            }

            if (missileCurrentTurnRate <= missileMaxTurnRate)
            {
                missileCurrentTurnRate += 1 * Time.deltaTime;
            }
        }
        
    }
}