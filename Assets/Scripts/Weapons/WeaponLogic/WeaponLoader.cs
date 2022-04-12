using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ammo;
using Ammo.AmmunitionBehaviors;
using Planes.AircraftBehavior;
using UnityEngine;
using Weapons.Interfaces;
using Missile = Ammo.Missile;

namespace Weapons.WeaponLogic
{
    public class WeaponLoader : MonoBehaviour, IShootMissile,IChangeWeapon
    {
        public List<Transform> weaponLocations = new List<Transform>();
        [SerializeField] private AircraftStatus aircraftStatus;
        [SerializeField] private AircraftSensor sensor;
        [SerializeField] private List<CurrentWeaponStatus> weapons;
        [SerializeField] private Transform[] targets;
        [SerializeField] private Transform[] lockedTargets;
        [SerializeField] private CurrentWeaponStatus[] weaponStatuses;
        [SerializeField] private int currentWeapon = 0;
        [SerializeField]private bool _isReloading = false;
        [SerializeField] private GameObject targetPlane;

        [SerializeField] private AudioSource _weaponSoundSource;
        [SerializeField] private bool reverseTargets = false;

        //Ready Weapon
        //Fire Weapon
        //Reload Weapon
        private void Awake()
        {
            aircraftStatus = GetComponent<AircraftStatus>();
            sensor = GetComponent<AircraftSensor>();
            targets = new Transform[10];
            lockedTargets = new Transform[2];
            weaponStatuses = aircraftStatus.weaponStatus.ToArray();
            _weaponSoundSource = GetComponent<AudioSource>();
            SetupWeapon();

        }

        

        private void LateUpdate()
        {
            targets = sensor.SendTargets().Distinct().Where(t => t != null).ToArray();
            if (weaponStatuses[currentWeapon].currentReloadSpeed >= 0)
                weaponStatuses[currentWeapon].currentReloadSpeed -= Time.deltaTime;
            else if (weaponStatuses[currentWeapon].isReloading)
                weaponStatuses[currentWeapon].isReloading = false;
        }

        // Cycle through Missile/Bombbays
        private List<Transform> CycleBays(string weaponBay) => weaponLocations.Where(location => location.CompareTag(weaponBay)).ToList();

        private void ReadyMissiles()
        {
            
            if(!_isReloading)
                StartCoroutine(ReloadMissile());
        }

        // from list of targets
        // 
        private void AutoLockTargets(Transform[] sentTargets)
        {
            for (var i = 0; i < lockedTargets.Length; i++) lockedTargets[i] = null;

            if (reverseTargets)
                sentTargets = sentTargets.Reverse().ToArray();
            for (var i = 0; i < sentTargets.Length; i++)
                if (!lockedTargets.Contains(sentTargets[i]) && i < lockedTargets.Length)
                    lockedTargets[i] = sentTargets[i];

            // lockedTargets = lockedTargets.Distinct().Where(t => t != null).ToArray();
        }

        private IEnumerator ReloadMissile()
        {
            _isReloading = true;
            var missileIndex = weaponStatuses.ToList().FindIndex(x=> x.type==WeaponType.MissileComputer); 
            yield return new WaitForSeconds(weaponStatuses[missileIndex].reloadSpeed);
            weaponStatuses[missileIndex].currentAmmo = weaponStatuses[missileIndex].numberOfBays;
            _isReloading = false;

        }

        // never thought that this will be complicated
        public void ShootMissile(Transform[] missileTargets)
        {
            AutoLockTargets(missileTargets);
            var bays = CycleBays("MissileBays");
            var filteredLockedTargets = missileTargets.Length == 0
                ? new Transform[0]
                : lockedTargets.Where(t => t != null).ToArray();

            if (weaponStatuses[currentWeapon].currentAmmo <= 0)
            {
                ReadyMissiles();
                return;
            }

            if (filteredLockedTargets.Length > 1)
            {
                for (var i = 0; i < filteredLockedTargets.Length; i++)
                {
                    targetPlane = filteredLockedTargets[i].gameObject;
                    weaponStatuses[currentWeapon].currentAmmo--;
                    if (weaponStatuses[currentWeapon].currentAmmo < 0) break;
                    SpawnProjectile(bays[weaponStatuses[currentWeapon].currentAmmo]);
                    lockedTargets[i] = null;
                }

                reverseTargets = !reverseTargets;
            }
            else if (filteredLockedTargets.Length == 1)
            {
                targetPlane = filteredLockedTargets[0].gameObject;
                weaponStatuses[currentWeapon].currentAmmo--;
                if (weaponStatuses[currentWeapon].currentAmmo < 0) return;
                SpawnProjectile(bays[weaponStatuses[currentWeapon].currentAmmo]);
                if (lockedTargets.Contains(targetPlane.transform))
                    lockedTargets[lockedTargets.ToList().IndexOf(targetPlane.transform)] = null;
            }
            else
            {
                targetPlane = null;
                weaponStatuses[currentWeapon].currentAmmo--;
                if (weaponStatuses[currentWeapon].currentAmmo < 0) return;
                SpawnProjectile(bays[weaponStatuses[currentWeapon].currentAmmo]);
                lockedTargets[0] = null;
            }

            if (weaponStatuses[currentWeapon].currentAmmo <= 0) ReadyMissiles();
        }


        public void ShootWeapon()
        {
            weaponStatuses[currentWeapon].currentTimer += Time.deltaTime;
            switch (weaponStatuses[currentWeapon].type)
            {
                case WeaponType.RocketPods:
                    break;
                case WeaponType.MissileComputer: // Suggesting convert to Coroutine
                    ShootMissile(targets);
                    break;
                case WeaponType.MachineGun:
                    const string localTag = "MachineGunPosition";
                    var machinegunPositions = weaponLocations.Where(i => i.CompareTag(localTag));
                    if (weaponStatuses[currentWeapon].currentTimer >= weaponStatuses[currentWeapon].fireRate && Time.deltaTime != 0)
                    {
                       
                        weaponStatuses[currentWeapon].currentTimer = 0f;
                        _weaponSoundSource.Play(); // move to Control
                        weaponStatuses[currentWeapon].currentAmmo--;
                        foreach (var position in machinegunPositions)
                        {
                            SpawnProjectile(position);
                        }
                    }

                    break;
                case WeaponType.Cannon:
                    break;
                case WeaponType.BombBay:
                    break;
                default:
                    break;
            }
        }


        private void SetupWeapon()
        {
            
            GetWeaponSpecificProperties();
            if(CurrentWeaponType() == WeaponType.MissileComputer && weaponStatuses[currentWeapon].currentAmmo <=0)
                ReadyMissiles();
            _weaponSoundSource.clip = weaponStatuses[currentWeapon].weaponAudio;
        }


        public void SpawnProjectile(Transform weaponPosition)
        {
            var ammo = weaponStatuses[currentWeapon].ammo;
            var projectile = Instantiate(ammo.ammoObject, weaponPosition.position, weaponPosition.rotation);
            switch (ammo.ammoType)
            {
                case AmmunitionType.Bullet:
                    projectile.AddComponent<BulletLogic>();
                    projectile.GetComponent<BulletLogic>().bulletDamage = ((Bullet) ammo).damage;
                    projectile.GetComponent<BulletLogic>().bulletSpeed = ((Bullet) ammo).bulletSpeed;
                    projectile.GetComponent<BulletLogic>().bulletMaxRange = ((Bullet) ammo).maxRange;
                    break;

                case AmmunitionType.Rocket:
                    break;

                case AmmunitionType.Missile:
                    var missile = (Missile) ammo;
                    var missileProperties = projectile.GetComponent<MissileLogic>();
                    if (targetPlane != null)
                        missileProperties.SetupMissile(targetPlane, missile.missileMaxRange,
                            missile.missileTopSpeed, missile.missileAcceleration, missile.missileMaxTurnRate,
                            missile.missileMinimumTurnRate, missile.missileSeekTime, missile.damage,missile.missileLaunchSound);
                    else
                        missileProperties.SetupMissile(missile.missileMaxRange,
                            missile.missileTopSpeed, missile.missileAcceleration, missile.missileMaxTurnRate,
                            missile.missileMinimumTurnRate, missile.missileSeekTime, missile.damage,missile.missileLaunchSound);
                    missileProperties.PlayLaunchSound();
                    break;

                case AmmunitionType.Bomb:
                    break;

                case AmmunitionType.Laser:
                    break;
                default:
                    break;
            }
        }

        public CurrentWeaponStatus ReturnStatus()
        {
            return weaponStatuses[currentWeapon];
        }

        public void Reload()
        {
            if (weaponStatuses[currentWeapon].currentAmmo < weaponStatuses[currentWeapon].weaponClipSize && weaponStatuses[currentWeapon].maximumAmmo > 0
            ) // 14/15 cs = 30 
            {
                var toReload = weaponStatuses[currentWeapon].weaponClipSize - weaponStatuses[currentWeapon].currentAmmo;
                if (toReload > weaponStatuses[currentWeapon].maximumAmmo) // 16 > 15
                    toReload = weaponStatuses[currentWeapon].maximumAmmo; //tR = 15   
                weaponStatuses[currentWeapon].maximumAmmo -= toReload; //15 - 15 
                weaponStatuses[currentWeapon].currentAmmo += toReload; //
                weaponStatuses[currentWeapon].isReloading = false;
            }
        }

        private void GetWeaponSpecificProperties()
        {
            sensor.SetRange(weaponStatuses[currentWeapon].viewRange);
            sensor.SetFOV(weaponStatuses[currentWeapon].fieldOfView);

            switch (weaponStatuses[currentWeapon].type)
            {
                case WeaponType.MachineGun:
                    weaponStatuses[currentWeapon].currentAmmo = weaponStatuses[currentWeapon].maximumAmmo;
                    break;
                case WeaponType.RocketPods:
                    break;
                case WeaponType.MissileComputer:
                    weaponStatuses[currentWeapon].weaponClipSize = weaponStatuses[currentWeapon].numberOfBays;
                    weaponStatuses[currentWeapon].currentReloadSpeed = weaponStatuses[currentWeapon].reloadSpeed;
                    ;
                    break;
                case WeaponType.Cannon:
                    break;
                case WeaponType.BombBay:
                    break;
                default:
                    break;
            }
        }

        public WeaponType CurrentWeaponType() => weaponStatuses[currentWeapon].type;
        public void ChangeWeapon()
        {
            currentWeapon = ++currentWeapon == weaponStatuses.Length ? 0 : currentWeapon;
            SetupWeapon();
        }

        public int GetCurrentAmmo() => weaponStatuses[currentWeapon].currentAmmo;

    }
}