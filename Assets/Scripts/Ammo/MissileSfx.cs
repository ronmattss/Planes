using System;
using UnityEngine;

namespace Ammo
{
    public class MissileSfx : MonoBehaviour
    {
        // Controls Missile Audio

        private GameObject plane;
        private float distanceToFloat;
        private float currentVolume = 1;
        private int currentDistanceNormalized;
        [SerializeField] private AudioSource sfxSource;
        

        private void OnEnable()
        {
            plane = GameplayManager.Instance.plane;
            currentDistanceNormalized = (int) Vector3.Distance(plane.transform.position, this.transform.position); // get initial Distance
            sfxSource = GetComponent<AudioSource>();
            sfxSource.clip = AudioManager.instance.GetSound(SFX.MissileEngine.ToString());
            sfxSource.Play();
            sfxSource.volume = currentVolume;
            

        }
        
        void Start()
        {
                    plane = GameplayManager.Instance.plane;
                    currentDistanceNormalized = (int) Vector3.Distance(plane.transform.position, this.transform.position); // get initial Distance
                    sfxSource = GetComponent<AudioSource>();
                    sfxSource.clip = AudioManager.instance.GetSound(SFX.MissileEngine.ToString());
                    sfxSource.Play();
                    sfxSource.volume = currentVolume;
        }
        

        void Update()
        {
           
          //  AdjustVolume();
            
        }

        void AdjustVolume()
        {
            if (currentDistanceNormalized <= Vector3.Distance(plane.transform.position, this.transform.position))
            {
                if(sfxSource.volume <= 1)
                    sfxSource.volume += Time.deltaTime  / currentDistanceNormalized;
            }
            else if (currentDistanceNormalized >= Vector3.Distance(plane.transform.position, this.transform.position))
            {
                if(sfxSource.volume >= 0)
                    sfxSource.volume -= Time.deltaTime / currentDistanceNormalized;
            }
            currentDistanceNormalized = (int) Vector3.Distance(plane.transform.position, this.transform.position);
            Debug.Log("Distance: "+ currentDistanceNormalized);
        }


    }
}