using System.Collections.Generic;
using Planes.AircraftBehavior;
using UnityEngine;
using Weapons;

namespace Planes.AircraftData
{
    [CreateAssetMenu(menuName = "Aircraft/New Aircraft")]
    public class Aircraft : ScriptableObject
    {
        public PlaneStats aircraftStats;
        public Sprite planeSprite;
        public AudioClip planeEngine;
        public List<Weapon> weapons = new List<Weapon>();
       // public Animator animator;

        public CurrentAircraftStatus SetAircraftStatus()
        {
           
            return new CurrentAircraftStatus(aircraftStats.health, aircraftStats.armor, aircraftStats.topSpeed,
                aircraftStats.turnRate,weapons);
        }
    }
}