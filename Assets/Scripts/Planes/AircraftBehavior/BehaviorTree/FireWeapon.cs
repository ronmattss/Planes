using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Planes.AircraftBehavior;
using Planes.AircraftBehavior.BehaviorTree;
using UnityEngine;

public class FireWeapon : Action
{

    public SharedAutoPilot pilot;

    public override TaskStatus OnUpdate()
    {
        pilot.Value.GetWeaponsControl().ShootWeapon();
        return TaskStatus.Running;
    }
}
