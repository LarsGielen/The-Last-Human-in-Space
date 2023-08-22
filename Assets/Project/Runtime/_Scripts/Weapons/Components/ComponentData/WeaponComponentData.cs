using System;
using UnityEngine;

namespace Project.Weapons.Components
{
    [Serializable]
    public class WeaponComponentData
    {
        [SerializeField][HideInInspector] private string name;
        public WeaponComponentData() => name = GetType().Name;
    }
}
