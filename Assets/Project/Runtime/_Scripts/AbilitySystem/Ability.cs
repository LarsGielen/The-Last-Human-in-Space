using System;
using UnityEngine;

using Project.Entity.CoreSystem;

namespace Project.AbilitySystem
{
    public class Ability : MonoBehaviour
    {
        [field: SerializeField] public GameObject BaseObject { get; private set; }

        private Core core;
        
        public AbilityDataSO Data { get; private set; }
        public Core Core
        {
            get
            {
                if (core != null) return core;
                else
                {
                    Debug.LogWarning($"{gameObject.name} has not set a core!");
                    return null;
                }
            }

            set => core = value;
        }

        public event Action OnEnter;
        public event Action OnExit;

        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }

        public void SetData(AbilityDataSO data)
        {
            this.Data = data;
        }
    }
}
