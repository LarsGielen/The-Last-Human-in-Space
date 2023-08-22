using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Weapons.Components
{
    public class DamageData : WeaponComponentData
    {
        public DamageData() => ComponentDependency = typeof(DamageComponent);

        [SerializeField] private float damage;

        public float Damage { get => damage; }
    }
}
