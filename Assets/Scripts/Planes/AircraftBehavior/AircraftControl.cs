using System;
using Ammo.AmmunitionBehaviors;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Planes.AircraftBehavior
{
    //Controls The Plane
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AircraftStatus))]
    public class AircraftControl : MonoBehaviour,ISpeedControl,ISteerProjectile
    {
        
        [SerializeField]private AircraftStatus _aircraftStatus;
        [SerializeField]private float _topSpeed;
        private float _handling;
        [SerializeField]private float _acceleration;
        [SerializeField]private float currentSpeed;
        [SerializeField]private float _minSpeed;
        private Rigidbody2D _rigidbody2D;
        public Text text;
        public GameObject other;
        void Start()
        {
            
            InitializeData();
        }

       public void InitializeData()
        {
            _aircraftStatus = this.gameObject.GetComponent<AircraftStatus>();
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            
            _topSpeed = _aircraftStatus.currentStatus.currentTopSpeed;
            _handling = _aircraftStatus.currentStatus.aircraftTurnRate;
            
            _minSpeed = _topSpeed / 2;
            currentSpeed = _topSpeed;
            _acceleration = _topSpeed - currentSpeed;
//            Debug.Log(_acceleration);
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = transform.up * currentSpeed;
         //   var targetsAngle = ProjectileComputation.GetAngleOfTarget(transform.position,other.transform.position);
         //   var computedAngle = targetsAngle + transform.rotation.z + 180;
          //  Debug.Log("Angle between this plane and static plane: "+ targetsAngle + " Computed Angle: "+ computedAngle);
        }

        
        #region AircraftMovementLogic
        public void Accelerate()
        {
            if (currentSpeed <= _topSpeed)
            {
                currentSpeed += _acceleration * Time.deltaTime;
            }
        }

        public void Decelerate()
        {
            if (currentSpeed >= _minSpeed)
            {
                currentSpeed -= _acceleration * Time.deltaTime;
            }
        }

        public void Turn(float direction)
        {
            var rateOfTurn = ProjectileComputation.TurnRate(direction, _handling, currentSpeed);
             transform.Rotate(0,0,rateOfTurn);
        }
        
        public void JoyStickTurn(float direction)
        {
            var rateOfTurn = ProjectileComputation.TurnRate(1, _handling, currentSpeed);
            var turnRate = GetTargetBasedEndAngle(Time.fixedDeltaTime, direction);
//            Debug.Log($"rate of turn with direction is degrees: {((360 - direction) / 60) * rateOfTurn} {(rateOfTurn ) * Time.deltaTime} {direction}");
            Quaternion q = Quaternion.AngleAxis(direction, transform.forward);
            this.transform.rotation = Quaternion.Slerp(transform.rotation,q, rateOfTurn * Time.fixedDeltaTime);
           // transform.Rotate(0,0,rateOfTurn);
        }
        // To compute current angle of planes add 180 to the target angle

        #endregion
        
        private float GetTargetBasedEndAngle(float deltaTime,float targetAngle)
        {
            float num = Mathf.Atan2(this._rigidbody2D.velocity.y, this._rigidbody2D.velocity.x);
            float num2 = Mathf.DeltaAngle(num * 57.29578f, targetAngle * 57.29578f) * 0.017453292f;
            float num3 = targetAngle;
            float angularSpeed = 1;
            float num4 = Mathf.Abs(num2);
            if (num4 > 0.001f || num4 - 6.2831855f > 0.001f)
            {
                if (num2 > 3.1415927f)
                {
                    num2 -= 6.2831855f;
                }
                else if (num2 < -3.1415927f)
                {
                    num2 = 6.2831855f - num2;
                }
                if (num4 > angularSpeed  * deltaTime)
                {
                    num3 = num + Mathf.Sign(num2) * angularSpeed *  deltaTime;
                }

            }
            else
            {
               // this.ReduceTilt(deltaTime);
            }
            return num3;
        }

       public float GetCurrentSpeed() => currentSpeed;
       public void SetCurrentSpeed(float speed) => currentSpeed = speed;

       public float GetMinSpeed() => _minSpeed;
       public float GetMaxSpeed() => _topSpeed;
       public float GetHandling() => _handling;
       public AircraftStatus GetAircraftStatus() => _aircraftStatus;




    }
}