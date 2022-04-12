using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Interfaces
{
    public interface IShootMissile
    {
        void ShootMissile(Transform[] missileTargets);
    }
}