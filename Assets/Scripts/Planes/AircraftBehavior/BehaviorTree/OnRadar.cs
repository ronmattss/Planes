using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Planes.AircraftBehavior.BehaviorTree
{
    public class OnRadar : Conditional
    {
        public SharedAutoPilot pilot;
        public string playerTag;

        public SharedTransform target;
        private Transform[] possibleTargets;
        public override void OnAwake()
        {
            pilot.Value = GetComponent<AircraftAutoPilot>();
            var targets = GameObject.FindGameObjectsWithTag(playerTag);
            possibleTargets = new Transform[targets.Length];
            for (int i = 0; i < targets.Length; ++i) {
                possibleTargets[i] = targets[i].transform;
            }
            
        }

        public override TaskStatus OnUpdate()
        {
            var posTargets = pilot.Value.GetSensor().SendRadarTargets();
            if (posTargets == null) return TaskStatus.Failure;
            if (posTargets.Length ==0) return TaskStatus.Failure;
            foreach (var posTarget in posTargets)
            {
                if (!posTarget.CompareTag(playerTag) && posTarget != null) continue;
                target.Value = posTarget;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}