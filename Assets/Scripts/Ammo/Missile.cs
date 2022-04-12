using System;
using UnityEngine;

namespace Ammo
{
    [CreateAssetMenu(menuName = "Ammunition/Missile")][Serializable]
    public class Missile : Ammunition
    {
        public float missileMaxRange;    // max range of missile before exploding
        public float missileSeekTime;  // missile follow time while turning
        public float missileTopSpeed;    // missile Top speed
        public float missileMaxTurnRate; // missile MaxTurnRate// missile Damage
        public float missileAcceleration;// this should be fast
        public float missileMinimumTurnRate; // min Turn Rate at top speed
        public AudioClip missileLaunchSound;

    }
}