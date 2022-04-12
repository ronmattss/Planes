using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Planes.AircraftBehavior.BehaviorTree
{
    public class TurnToTarget : Action
    {
        public SharedTransform target;
        public SharedAutoPilot AutoPilot;
        
        //Turn until thisangle is in the desired angle
        public override TaskStatus OnUpdate()
        {
           if(target.Value != null)
            AutoPilot.Value.CalculatedTurn(target.Value);
            return TaskStatus.Running;

            //  return thisAngle == targetAngle ? TaskStatus.Success : TaskStatus.Running;
            // We haven't reached the target yet so keep moving towards it
        }
    }
}