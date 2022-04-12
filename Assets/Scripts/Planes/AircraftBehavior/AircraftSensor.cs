using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Planes.AircraftBehavior
{
    [Serializable]
    public class AircraftSensor : MonoBehaviour
    {
        [Header("Missile Targeter Properties")]
        public Utility.FieldOfView field;

        public Transform[] targetsInRange;
        [SerializeField] private GameObject sensorLocation;
        [SerializeField] private float fov = 40; //should be dynamic
        [SerializeField] float viewDistance = 50f;
        [SerializeField] private LayerMask layer;

        [Header("Radar Properties")] [SerializeField]
        private GameObject radarLocation;

        [SerializeField] private float radarRadius = 1;
        [SerializeField] private LayerMask scannableLayer;
        [SerializeField] private Transform[] visibleEnemies;
        private RaycastHit2D[] radarHits;


        Vector3 origin;
        private RaycastHit2D[] raycasts;
        private Transform[] hittedTransforms;
        int rayCount = 50;
        float startingAngle;

        private void Awake()
        {
            raycasts = new RaycastHit2D[rayCount];
            hittedTransforms = new Transform[rayCount];
            targetsInRange = new Transform[rayCount];
            visibleEnemies = new Transform[rayCount];
            if (field != null)
            {
                field.SetFOV(fov);
                field.SetViewDistance(viewDistance);
            }

            InvokeRepeating(nameof(UseSensors), .5f, .5f);
            //viewDistance = 20f;
        }


        private void Update()
        {
            startingAngle = (sensorLocation.transform.rotation.eulerAngles.z + fov / 2f) + 90;
            if (field != null)
            {
                field.SetStartingAngle(sensorLocation);
                field.SetOrigin(sensorLocation);
            }


            //  field.origin = this.transform.position;
        }

        public Transform[] SendTargets() => targetsInRange;
        public Transform[] SendRadarTargets() => visibleEnemies;

        private void UseSensors()
        {
            ScanTargets();
            RadarScan();
        }

        private void ScanTargets()
        {
            origin = sensorLocation.transform.position;

            float angle = startingAngle;
            float angleIncrease = fov / rayCount;
            for (int i = 0; i < rayCount; i++)
            {
                raycasts[i] = Physics2D.Raycast(origin, ProjectileComputation.AngleToVector(angle), viewDistance,
                    layer);
                if (raycasts[i].collider != null)
                {
                    var plane = raycasts[i].collider.transform;
                    if (!hittedTransforms.Contains(plane))
                    {
                        hittedTransforms[i] = plane;
                    }

                    if (!targetsInRange.Contains(plane))
                    {
                        hittedTransforms[i] = plane;

                        //IF a plane is hit by a raycast, add it to targets
                    }
                }
                else
                {
                    hittedTransforms[i] = null;
                }

                angle -= angleIncrease;
            }

            targetsInRange = hittedTransforms.Distinct().ToArray();
        }

        void RadarScan()
        {
            radarHits = Physics2D.CircleCastAll(radarLocation.transform.position, radarRadius, Vector2.zero,
                radarRadius, scannableLayer);
            Transform[] temp = new Transform[radarHits.Length];
            for (var index = 0; index < radarHits.Length; index++)
            {
                var hits = radarHits[index];
                temp[index] = hits.transform;
            }

            temp = temp.Distinct().ToArray();
            visibleEnemies = temp;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(radarLocation.transform.position, radarRadius);
            origin = sensorLocation.transform.position;
            int rayCount = 50;
            float angle = startingAngle;
            float angleIncrease = fov / rayCount;
            for (int i = 0; i <= rayCount; i++)
            {
                //Debug.Log("Angle: "+angle+" EulerAngle: "+startingAngle);

                RaycastHit2D raycastHit = Physics2D.Raycast(origin, ProjectileComputation.AngleToVector(angle),
                    viewDistance, layer);
                if (raycastHit.collider == null)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(origin, ProjectileComputation.AngleToVector(angle) * viewDistance);
                }
                else
                {
                    // Gizmos.DrawRay( origin, ProjectileComputation.AngleToVector(Vector2.Distance(raycastHit.point,origin)));
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(origin,
                        new Vector3(origin.x - raycastHit.point.x, origin.y - raycastHit.point.y) * -1);
                    Debug.Log("Collided with: " + raycastHit.collider.transform.name);
                }

                angle -= angleIncrease;
            }
        }

        public void SetStartingAngle(Vector3 aimDirection)
        {
            startingAngle = ProjectileComputation.GetAngleFromVectorFloat(aimDirection) - fov / 2f;
        }

        public void SetFOV(float view)
        {
            fov = view;
            if (field != null)
                field.SetFOV(view);
        }

        public void SetRange(float range)
        {
            viewDistance = range;
            if (field != null)
                field.SetViewDistance(range);
        }
    }
}