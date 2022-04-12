using System;
using System.Collections.Generic;
using Weapons;

namespace Planes.AircraftBehavior
{
    [Serializable]
    public struct CurrentAircraftStatus
    {
        public CurrentAircraftStatus(float health,float armor,float topSpeed,float handling,List<Weapon> weapons)
        {
            currentHealth = health;
            currentArmor = armor;
            currentTopSpeed = topSpeed;
            aircraftTurnRate = handling;
            aircraftWeapons = weapons;
        }
        public float currentHealth;
        public float currentArmor;
        public float currentTopSpeed;
        public float aircraftTurnRate;
        public List<Weapon> aircraftWeapons;

    }
}