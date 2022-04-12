using UnityEngine;

namespace Ammo
{
    public class Ammunition : ScriptableObject
    {
        public string ammunitionName;
        public float damage;
        public int maxAmmo;
        public GameObject ammoObject;
        public AmmunitionType ammoType;
        
        
    }

    public enum AmmunitionType
    {
        Bullet,Rocket,Missile,Bomb,Laser
    }
}