using System;
using UnityEngine;

namespace Project.Weapons.Components
{
    [Serializable]
    public abstract class WeaponComponentData
    {
        public WeaponComponentData() => name = GetType().Name;

        [SerializeField][HideInInspector] private string name;

        public Type ComponentDependency { get; protected set; }
    }
}
