using System;
using System.Collections.Generic;
using DefaultNamespace.Pickups;
using Managers;
using Planes.AircraftData;
using UnityEditor;
using UnityEngine;
using Weapons;

namespace Planes.AircraftBehavior
{
    public class AircraftStatus : MonoBehaviour
    {
        // MonoBehavior that Serializes aircraft data 
        [SerializeField] private Aircraft aircraft;
        [SerializeField] private List<Weapon> weaponList = new List<Weapon>();
        [SerializeField] private AudioSource engineSource;

        public List<CurrentWeaponStatus> weaponStatus = new List<CurrentWeaponStatus>();
        public CurrentAircraftStatus currentStatus;
        private SpriteRenderer spriteRenderer;

       // private Animation animation;

        private void Awake()
        {
          //  animation = GetComponent<Animation>();
           // animation.clip = aircraft.animation;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = aircraft.planeSprite;
            engineSource.clip = aircraft.planeEngine;
            currentStatus = aircraft.SetAircraftStatus();
            weaponList = currentStatus.aircraftWeapons;
            InitializeWeapons();
        }

        private void OnEnable()
        {
            currentStatus = aircraft.SetAircraftStatus();

        }

        public void SetAircraft(Aircraft selectedProperties)
        {
            aircraft = selectedProperties;
            spriteRenderer.sprite = aircraft.planeSprite;
            currentStatus = aircraft.SetAircraftStatus();
            engineSource.clip = aircraft.planeEngine;
            engineSource.Play();


        }



        private void InitializeWeapons()
        {
            foreach (var weapon in weaponList)
            {
                weaponStatus.Add(weapon.SetWeaponStatus());
            }
        }

        public void SetHealth(float damage)
        {
            currentStatus.currentHealth -= damage;
            if(currentStatus.currentHealth <=0)
                this.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            GameplayManager.Instance.ResetGameplay();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out OnPickUp pickup))
            {
                pickup.PickUp();
            }
        }


        // Update is called once per frame

        
        
        
    }
}
