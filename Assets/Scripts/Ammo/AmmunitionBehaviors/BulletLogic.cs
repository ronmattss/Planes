using System;
using Planes.AircraftBehavior;
using UnityEngine;

namespace Ammo.AmmunitionBehaviors
{
    public class BulletLogic : MonoBehaviour,IPropelProjectile
    {
        // Get Bullet Data
        // Travel Logic
        //Oncollision logic
        // Destroy logic

        public Rigidbody2D bulletBody;
        public float bulletMaxRange;
        public float bulletSpeed;
        public float bulletDamage;

        private void Start()
        {
            bulletBody = gameObject.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
      //      Debug.Log("what is other? "+ other.transform.name);
      if (other.gameObject != null)
      {
          other.GetComponent<AircraftStatus>().SetHealth(bulletDamage);
          Destroy(gameObject);
      }

        }

        private void OnDestroy()
        {
        //    Debug.Log("bullet Destroyed, Damage: " + bulletDamage);
        }

        public void Move()
        {
            if(bulletMaxRange>=0)
                bulletBody.velocity = transform.up * bulletSpeed;
            else
            {
                Destroy(gameObject);
            }
            bulletMaxRange -= Time.deltaTime;
            

        }
    }
}