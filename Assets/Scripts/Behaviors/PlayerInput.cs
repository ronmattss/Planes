using System;
using Planes.AircraftBehavior;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using Weapons;
namespace Behaviors
{
    public class PlayerInput : MonoBehaviour
    {
        // Initial controls for PC will be changed 
        public KeyCode accelerate;
        public KeyCode decelerate;
        public KeyCode turnLeft;
        public KeyCode turnRight;
        public KeyCode fireWeapon;
        public KeyCode changeWeapon;
        public AircraftControl planeControl;
        public AircraftWeaponsControl planeWeaponControl;
        public Joystick stick;

        private void Awake()
        {
            planeControl = GetComponent<AircraftControl>();
            planeWeaponControl = GetComponent<AircraftWeaponsControl>();
        }

        //Refactor in the feature
        private void Update()
        {
            float angleOfStick = Mathf.Atan2(stick.Direction.y, stick.Direction.x) * Mathf.Rad2Deg - 90;
         //   var rateOfTurn = ProjectileComputation.TurnRate(direction, _handling, currentSpeed);
          //  Debug.Log(stick.Direction + " degree: "+ angleOfStick );
            if(stick.Direction != Vector2.zero)
               planeControl.JoyStickTurn(angleOfStick);
            
//            FireWeapon(planeWeaponControl.type);
            if(Input.GetKey(accelerate))
                planeControl.Accelerate();
            if(Input.GetKey(decelerate))
                planeControl.Decelerate();
            if(Input.GetKey(turnLeft))
                planeControl.Turn(1);
            if(Input.GetKey(turnRight))
                planeControl.Turn(-1);
            if (Input.GetKeyUp(changeWeapon))
                planeWeaponControl.changeWeapon.ChangeWeapon();
        }

        private void FireWeapon(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.RocketPods:
                    if (Input.GetKeyUp(fireWeapon))
                        planeWeaponControl.ShootWeapon();
                    break;
                case WeaponType.MissileComputer:
                    if (Input.GetKeyUp(fireWeapon))
                        planeWeaponControl.ShootWeapon();
                    break;
                case WeaponType.MachineGun:
                    if (Input.GetKey(fireWeapon))
                        planeWeaponControl.ShootWeapon();
                    break;
                case WeaponType.Cannon:
                    break;
                case WeaponType.BombBay:
                    if (Input.GetKeyUp(fireWeapon))
                        planeWeaponControl.ShootWeapon();
                    break;
                default:
                    return;
            }
        }
    }
}