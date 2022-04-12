using System;
using UnityEngine;

namespace DefaultNamespace.Pickups
{
    public class CoinPickup : OnPickUp
    {
        public int coinValue = 1;
        private void OnEnable()
        {
            this.gameObject.transform.parent = null;
        }

        public override void Update()
        {
            base.Update();
           
        }

        public override void PickUp()
        {
            var currScore =  UIManager.Instance.GetDisplayScore();
            currScore += coinValue;
            UIManager.Instance.SetScore(currScore);
            Destroy(this.gameObject);
        }
    }
}