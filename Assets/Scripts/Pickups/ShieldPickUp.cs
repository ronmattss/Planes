using DefaultNamespace.Pickups;
using Planes.AircraftBehavior;
using UnityEngine;

namespace Pickups
{
    public class ShieldPickUp : OnPickUp
    {
        public GameObject shield;

        public override void PickUp()
        {
            var powerUpController = GameplayManager.Instance.plane.GetComponent<AircraftPowerUpControl>();
            if (powerUpController.activePowerUp)
            {
                var currScore = UIManager.Instance.GetDisplayScore();
                currScore++;
                UIManager.Instance.SetScore(currScore);
            }
            else
            {
                powerUpController.hasShield = true;
                var shieldPrefab = Instantiate(shield, Vector3.zero, Quaternion.identity);
                // shieldPrefab.SetActive(true);
               // powerUpController.powerUpDuration = 9999;
                shieldPrefab.gameObject.transform.parent = GameplayManager.Instance.plane.transform;
                shieldPrefab.gameObject.transform.localPosition = Vector3.zero;
            }
            Destroy(this.gameObject);
        }
    }
}