using System;
using Ammo;
using Planes.AircraftBehavior;
using UnityEngine;

namespace Weapons
{
    
    public class Weapon: ScriptableObject
    {
        public string weaponModel;
        public float reloadSpeed;
        public WeaponType type;
        public float fireRate;
        public Ammunition ammo;
        public int clipSize;
        public AudioClip shootingSound;
        public float fieldOfView;
        public float viewRange;
        

        public CurrentWeaponStatus SetWeaponStatus()
        {
            var weaponStatus = new CurrentWeaponStatus
            {
                type = this.type,
                weaponModel = this.weaponModel,
                ammo = this.ammo,
                spread = this.type == WeaponType.MachineGun ? ((MachineGun)this).spread : 0f,
                reloadSpeed = reloadSpeed,
                isReloading = false,
                weaponClipSize = clipSize,
                currentAmmo = clipSize,
                maximumAmmo = ammo.maxAmmo,
                currentReloadSpeed = 0,
                currentTimer = 0,
                fireRate = fireRate,
                weaponAudio = shootingSound,
                numberOfBays = (type == WeaponType.MissileComputer || type == WeaponType.BombBay) ? ((MissileBay) this).numberOfBays :0,
                viewRange = viewRange,
                fieldOfView = fieldOfView
            };
            return weaponStatus;
        }
    }

    public enum WeaponType
    {
        RocketPods,MissileComputer,MachineGun,Cannon,BombBay
    }
}