using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "Weapons/New MissileBay")]
    public class MissileBay: Weapon
    {
        public int numberOfBays; // clipSize
        public int maximumTargets = 1;
    }
}