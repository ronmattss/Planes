using System;
using Ammo;
using UnityEngine;
using Weapons;

namespace Planes.AircraftBehavior
{
    [Serializable]
    public struct CurrentWeaponStatus
    {
        public WeaponType type;
        public string weaponModel;
        public float fireRate;
        public float spread;
        public int weaponClipSize;
        public int maximumAmmo;
        public int currentAmmo;
        public float currentTimer;
        public float reloadSpeed;
        public float currentReloadSpeed;
        public Ammunition ammo;
        public AudioClip weaponAudio;
        public int numberOfBays;
        public bool isReloading;
        public float fieldOfView;
        public float viewRange;
    }
}