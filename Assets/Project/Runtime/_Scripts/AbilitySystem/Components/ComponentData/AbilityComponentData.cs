using System;
using UnityEngine;

namespace Project.AbilitySystem.Components
{
    [Serializable]
    public abstract class AbilityComponentData
    {
        public AbilityComponentData()
        {
            name = GetType().Name;
            SetComponentDependency();
        }

        [SerializeField][HideInInspector] private string name;

        public Type ComponentDependency { get; protected set; }

        protected abstract void SetComponentDependency();
    }
}
