using System;
using System.Collections.Generic;
using Ammo;
using Ammo.AmmunitionBehaviors;
using UnityEngine;
using Weapons;
using Weapons.Interfaces;
using Weapons.WeaponLogic;

namespace Planes.AircraftBehavior
{
    public class AircraftWeaponsControl : MonoBehaviour
    {
        // make this behavior just control weapons
    [SerializeField]    private List<CurrentWeaponStatus> WeaponStatuses = new List<CurrentWeaponStatus>();
    [SerializeField]private List<GameObject> listOfTargets = new List<GameObject>();
        private WeaponLoader _weaponLoader;
        private AircraftStatus planeStatus;
    private IShootMissile _shootMissile;
    public IChangeWeapon changeWeapon;
    public WeaponType type;





        private void Start()
        {
            planeStatus = GetComponent<AircraftStatus>();
            WeaponStatuses =planeStatus.weaponStatus;
            
            _weaponLoader = GetComponent<WeaponLoader>();
            _shootMissile = _weaponLoader;
             changeWeapon = _weaponLoader;
//                _weaponLoader.ReadyWeapon(WeaponStatuses[0],listOfTargets);
        }

        private void FixedUpdate()
        {
            type = _weaponLoader.CurrentWeaponType();
        }



        public void ShootWeapon()
        {
            _weaponLoader.ShootWeapon();
        }
        



        
    }
}