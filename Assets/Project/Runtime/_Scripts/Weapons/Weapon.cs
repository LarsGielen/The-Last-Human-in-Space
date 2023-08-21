using System;
using UnityEngine;

namespace Project.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public GameObject WeaponBaseObject { get; private set; }
        [field: SerializeField] public WeaponDataSO Data { get; private set; }

        public event Action OnEnter;
        public event Action OnExit;

        public void Enter()
        {
            print($"Weapon Name: {transform.name}");

            OnEnter?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}
