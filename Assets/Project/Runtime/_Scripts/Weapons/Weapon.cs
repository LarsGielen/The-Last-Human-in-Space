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

        public event Action OnTriggerWeapon;

        public void Enter()
        {
            print($"Weapon Name: {Data.WeaponName}");

            OnEnter?.Invoke();

            // TODO: iets bedenken zodat deze na x seconden weer opnieuw wordt opgeroepen
            OnTriggerWeapon?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}
