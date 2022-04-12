using BehaviorDesigner.Runtime;

namespace Planes.AircraftBehavior.BehaviorTree
{
    [System.Serializable]
    public class SharedAutoPilot : SharedVariable<AircraftAutoPilot>
    {
        public static implicit operator SharedAutoPilot(AircraftAutoPilot value)
        {
            return new SharedAutoPilot
            {
                Value = value
            };
        }
    }
}