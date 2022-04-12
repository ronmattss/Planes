using Planes.AircraftData;
using UnityEngine;

namespace DefaultNamespace.Store
{
    [CreateAssetMenu(fileName = "PurchasableAircraft", menuName = "Store", order = 0)]
    public class Purchasable : ScriptableObject
    {
        public int price;
        public bool isUnlocked;
        public bool isInApp;
        public float realPrice;
        public string name;
        public Aircraft aircraft;
    }
}