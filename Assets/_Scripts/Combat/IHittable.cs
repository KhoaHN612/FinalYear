using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public interface IHittable
    {
        bool CanHit { get; set; }
        void GetHit(GameObject gameObject, int weaponDamage);
    }
}