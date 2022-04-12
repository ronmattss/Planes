using System;
using UnityEngine;

namespace Planes
{
    [Serializable]
    public class PlaneStats
    {
        public string model;
        public float topSpeed;
        public float acceleration;
        public float turnRate;
        public float health;
        public float armor;
        public AircraftClassification classification;
    }

    public enum AircraftClassification
    {
        Fighter,Bomber,MultiRole
    }
}
