using System;
using Managers;
using UnityEngine;

namespace DefaultNamespace.Pickups
{
    public abstract class OnPickUp : MonoBehaviour
    {
        public int duration = 7;
        private float _timer = 0;
        public abstract void PickUp();
        public virtual void Update()
        {
            if (GetComponent<Renderer>().isVisible)
            {
                GetComponent<Target>().enabled = false;
            }
            else
            {
                GetComponent<Target>().enabled = true;
            }

            if (_timer >= duration)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _timer += Time.deltaTime;
            }
            
        }

        public void OnDestroy()
        {
            SpawnManager.Instance.pickups.Remove(this);
        }


    }
}