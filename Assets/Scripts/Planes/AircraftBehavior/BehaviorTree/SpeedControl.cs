using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Planes.AircraftBehavior.BehaviorTree
{
    public class SpeedControl : Action
    {
        public SharedTransform target;
        public SharedAutoPilot pilot;
        private float randomDistance;
        
        public override void OnAwake()
        {
            randomDistance = pilot.Value.randomDistanceToTarget;
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value == null) return TaskStatus.Running;
            var distance = Vector3.Distance(target.Value.position, transform.position);

    

            if (distance > randomDistance)
            {
                pilot.Value.GetControls().Accelerate();
            }

            else 
                pilot.Value.GetControls().Decelerate();
            

//            Debug.Log("Distance between Target: "+ distance);
            return TaskStatus.Running;
        }
    }
}