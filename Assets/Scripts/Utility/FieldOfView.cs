using System;
using UnityEditor;
using UnityEngine;
namespace Utility
{
    public class FieldOfView : MonoBehaviour
    {

        private Mesh mesh;
        [SerializeField] private LayerMask layer;
        private float fov = 90f; //should be dynamic

       public Vector3 origin;
        float angle = 0f;
        float viewDistance = 50f;
        private float startingAngle;

        void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        private void LateUpdate()
        {
            //origin = this.transform.position;
            int rayCount = 50;
            float angle = startingAngle;
            float angleIncrease = fov / rayCount;
             

            Vector3[] vertices = new Vector3[rayCount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];

            vertices[0] = origin;

            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
               RaycastHit2D raycastHit = Physics2D.Raycast(origin, ProjectileComputation.GetVectorFromAngle(angle), viewDistance,layer); // object Interaction Delete if not needed
//                Gizmos.DrawRay(origin,ProjectileComputation.GetVectorFromAngle(angle));
               if (raycastHit.collider == null)
               {
                   vertex = origin + ProjectileComputation.GetVectorFromAngle(angle) * viewDistance;
               }
               else
               {
                   vertex = raycastHit.point;
               }
               
               vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
                
                angle -= angleIncrease;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

        }



        public void SetOrigin(GameObject aimDirection)
        {
            this.origin = aimDirection.transform.position;
        }

       public void SetStartingAngle(GameObject aimDirection)
        {
            startingAngle = (aimDirection.transform.rotation.eulerAngles.z + fov / 2f) + 90;
        }

       public void SetFOV(float fov)
       {
           this.fov = fov;
       }

       public void SetViewDistance(float distance)
       {
           viewDistance = distance;
       }
    }
}