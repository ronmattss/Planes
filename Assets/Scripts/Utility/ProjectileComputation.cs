using System;
using UnityEngine;

namespace Utility
{
    public static class ProjectileComputation
    {
        private const int MagicNumber = 1091;
        //Target Angle Computation
         public static float GetAngleOfTarget(Vector3 basePosition,Vector3 targetPosition)
        {
            var subtractedVectors = basePosition - targetPosition;
            var possibleAngle = (Mathf.Rad2Deg * Mathf.Atan2(subtractedVectors.y, subtractedVectors.x))-90;
            return possibleAngle;
        }

         public static Vector3 AngleToVector(float angle)
         {
             float angleRad = angle* (Mathf.PI / 180f) * 1;
             return new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad));
         }
         
         public static float GetAngleOfTarget(Vector3 basePosition)
         {
             var subtractedVectors = basePosition;
             var possibleAngle = (Mathf.Rad2Deg * Mathf.Atan2(subtractedVectors.y, subtractedVectors.x))-90;
             return possibleAngle;
         }

         public static float TurnRate(float direction,float handling,float speed)
         {
             //1091 is a magic constant
             var rateOfTurn = (MagicNumber*Mathf.Tan(direction * handling) / speed) *Time.deltaTime;
             return rateOfTurn;
         }
         
         public static Vector3 GetVectorFromAngle(float angle) {
             // angle = 0 -> 360
             float angleRad = angle * (Mathf.PI/180f);
             return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
         }

         public static float GetAngleFromVectorFloat(Vector3 dir) {
             dir = dir.normalized;
             float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
             if (n < 0) n += 360;

             return n;
         }
         
         public static int SafeDivision(float a, float b)
         {
             try
             {
                 return(int) Mathf.Sign(a / b);
             }
             catch (Exception e)
             {
                 Debug.Log("LMAO exception: "+e);
                 return 1;
             }
         }
    }
}