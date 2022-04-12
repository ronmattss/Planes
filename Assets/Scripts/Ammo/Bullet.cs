using UnityEngine;

namespace Ammo
{
    [CreateAssetMenu(menuName = "Ammunition/Bullet")]
    public class Bullet : Ammunition
    {
        public float maxRange;
        public float bulletSpeed;
    }
}