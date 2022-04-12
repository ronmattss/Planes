using System;
using System.Collections.Generic;

namespace DefaultNamespace.Store
{
    [Serializable]
    public class PurchasableData
    {
        public int price;
        public bool isUnlocked;
        public bool isInApp;
        public float realPrice;
        public string name;
    }
    [Serializable]
    public class PurchasedData
    {
        public List<PurchasableData> purchasableData;
    }
}