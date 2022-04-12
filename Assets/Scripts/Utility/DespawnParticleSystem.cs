using System;
using UnityEngine;

namespace Utility
{
    public class DespawnParticleSystem : MonoBehaviour
    {
        private bool flag = false;
        
        private void Update()
        {
            if (transform.parent == null && flag == false)
            {
                Destroy(this.gameObject,3f);
                this.transform.GetChild(0).gameObject.SetActive(true);
                flag = true;
            }
        }
    }
}