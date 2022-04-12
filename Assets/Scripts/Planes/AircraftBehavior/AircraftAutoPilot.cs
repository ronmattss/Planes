using System;
using UnityEngine;
using Utility;
using Weapons.WeaponLogic;
using Random = UnityEngine.Random;

namespace Planes.AircraftBehavior
{
    public class AircraftAutoPilot : MonoBehaviour
    {
        [SerializeField]private AircraftStatus status;
        [SerializeField]private AircraftControl control;
        [SerializeField]private AircraftWeaponsControl weaponControl;
        [SerializeField] private WeaponLoader loader;
        [SerializeField] private AircraftSensor sensor;
         public bool isTurning;
         public float setDistanceToTargetMax;
         public float randomDistanceToTarget;

        private void Awake()
        {
            randomDistanceToTarget = Random.Range(8, setDistanceToTargetMax);
            if (TryGetComponent(out AircraftStatus stats))
            {
                status = stats;
            }
            if (TryGetComponent(out AircraftControl component))
            {
                control = component;
            }
            if (TryGetComponent(out AircraftSensor radar))
            {
                sensor = radar;
            }
            if (TryGetComponent(out AircraftWeaponsControl weapon))
            {
                weaponControl = weapon;
            }
            if (TryGetComponent(out WeaponLoader load))
            {
                loader = load;
            }
        }

        
        public void CalculatedTurn(Transform target)
        {

            var plane = transform;
            var targetAngle =  
                ProjectileComputation.GetAngleOfTarget(plane.position, target.transform.position) + 180;    
            Turn(targetAngle);
           // Debug.Log("Angle: "+targetAngle);
        }
        public void TurnSimple(float direction)
        {
            var rateOfTurn = ProjectileComputation.TurnRate(direction, control.GetHandling(), control.GetCurrentSpeed());
            transform.Rotate(0,0,rateOfTurn);
        }

        
        public void Turn(float direction)
        {
            var previousAngle =(int) transform.rotation.eulerAngles.z;
            Quaternion q = Quaternion.AngleAxis(direction, transform.forward);
            var rateOfTurn = ProjectileComputation.TurnRate(1,control.GetHandling(),control.GetCurrentSpeed());
           
            Debug.Log("Rate Of Turn: "+rateOfTurn +" Angle: "+previousAngle +" turnRate: "+control.GetHandling() + " speed: "+ control.GetCurrentSpeed() +"Time: "+Time.deltaTime);
           // transform.Rotate(0,0,rateOfTurn);
           
           this.transform.rotation = Quaternion.SlerpUnclamped(transform.rotation,q,rateOfTurn * Time.deltaTime);
            

             var currentAngle =(int) transform.rotation.eulerAngles.z;
            if (previousAngle == currentAngle)
            {

                isTurning = false;
            }
            else
            {
                isTurning = true;
                control.Decelerate();
            }
            // Debug.Log($"previousAngle:{previousAngle} currentAngle: {currentAngle}");

        }
        public AircraftStatus GetStatus() => status;
        public AircraftControl GetControls() => control;
        public AircraftWeaponsControl GetWeaponsControl() => weaponControl;
        public WeaponLoader GetLoader() => loader;
        public AircraftSensor GetSensor() => sensor;
    }
}