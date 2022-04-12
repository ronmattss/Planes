using System;
using DefaultNamespace;
using UnityEngine;
using Weapons;
using Weapons.WeaponLogic;

namespace Planes.AircraftBehavior
{
    public class AirplaneHud : MonoBehaviour
    {
        public WeaponLoader weaponLoader;
        public AircraftControl controls;

        

        private void LateUpdate()
        {
    //        UIManager.Instance.speedText.text = $"Current Speed: {(int)controls.GetCurrentSpeed()}";
      //      UIManager.Instance.ammoText.text = $"Current Ammo: {weaponLoader.GetCurrentAmmo()}";
            UIManager.Instance.weaponText.text = $"Current Weapon: {GetWeaponType(weaponLoader.CurrentWeaponType())}";
        }

        private String GetWeaponType(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.RocketPods:
                    return "null";
                case WeaponType.MissileComputer:
                    return "Missiles";
                case WeaponType.MachineGun:
                    return "Machine Gun";
                case WeaponType.Cannon:
                    return "null";
                case WeaponType.BombBay:
                    return "null";
                default:
                    return "null";
            }
        }
    }
}