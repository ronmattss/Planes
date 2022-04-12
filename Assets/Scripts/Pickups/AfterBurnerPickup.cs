using DefaultNamespace.Pickups;
using Planes.AircraftBehavior;

namespace Pickups
{
    public class AfterBurnerPickup : OnPickUp
    {
        public float speedDuration = 3;

        public int turboSpeed = 5;

        // Random Power up 
        public override void PickUp()
        {
            // Change current speed to currSpeed+= turbospeed
            // after duration return to currSpeed
            var pickUp = GameplayManager.Instance.plane.GetComponent<AircraftPowerUpControl>();
            if (!pickUp.hasShield && pickUp.activePowerUp)
            {
                var currScore = UIManager.Instance.GetDisplayScore();
                currScore++;
                UIManager.Instance.SetScore(currScore);
            }
            else
            {
                pickUp.speedBoost = turboSpeed;
                pickUp.planeControl.SetCurrentSpeed(turboSpeed + pickUp.planeControl.GetCurrentSpeed());
                pickUp.powerUpDuration = speedDuration;
                pickUp.activePowerUp = true;
            }
            Destroy(gameObject);
        }
    }
}