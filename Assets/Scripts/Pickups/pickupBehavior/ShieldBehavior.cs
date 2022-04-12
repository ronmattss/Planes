using System;
using DefaultNamespace.Pickups;
using Planes.AircraftBehavior;
using UnityEngine;

namespace Pickups.pickupBehavior
{
    public class ShieldBehavior : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out OnPickUp pickUp))
            {
                Debug.Log(other.name);
                pickUp.PickUp();
            }

            if (!other.CompareTag("Missile")) return;
           // GameplayManager.Instance.plane.GetComponent<AircraftPowerUpControl>().activePowerUp = false;
            GameplayManager.Instance.plane.GetComponent<AircraftPowerUpControl>().hasShield = false;
//            GameplayManager.Instance.plane.GetComponent<AircraftPowerUpControl>().powerUpDuration = 0;
            this.transform.parent = null;
            
            Destroy(other.gameObject);
            Destroy(this.gameObject);


        }
    }
}