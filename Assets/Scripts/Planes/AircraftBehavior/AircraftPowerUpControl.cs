using System;
using UnityEngine;

namespace Planes.AircraftBehavior
{
    public class AircraftPowerUpControl : MonoBehaviour
    {
        // Controls active Powerup
        public GameObject powerUpSlot; // slot for shield or flares or EMP object
        public float powerUpDuration;

        public bool hasShield = false;
        // for Speed up PowerUp
        public AircraftControl planeControl;
        public bool activePowerUp = false;
        private float currentSpeed;

        public float speedBoost;

        private void OnEnable()
        {
            currentSpeed = planeControl.GetCurrentSpeed();
        }

        private void FixedUpdate()
        {
            if (!activePowerUp) return;
           
            if (powerUpDuration <= 0)
            {
                planeControl.SetCurrentSpeed(planeControl.GetAircraftStatus().currentStatus.currentTopSpeed);
                activePowerUp = false;
            }
            else
            {
                powerUpDuration -= Time.deltaTime;
            }

        }
    }
}