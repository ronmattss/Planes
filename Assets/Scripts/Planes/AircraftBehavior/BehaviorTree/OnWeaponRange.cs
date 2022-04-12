using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Planes.AircraftBehavior.BehaviorTree
{
    public class OnWeaponRange : Conditional
    {
        public SharedTransform target;
        public SharedAutoPilot pilot;

        public override TaskStatus OnUpdate() =>  pilot.Value.GetSensor().targetsInRange.Any(possibleTargets => target.Value == possibleTargets && target.Value !=null) ? TaskStatus.Success : TaskStatus.Failure;
    }
}
