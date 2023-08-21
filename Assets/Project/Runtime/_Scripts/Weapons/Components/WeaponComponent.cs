using UnityEngine;

namespace Project.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        protected bool isAttacking;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
        }

        protected virtual void HandleEnter() => isAttacking = true;

        protected virtual void HandleExit() => isAttacking = false;

        protected virtual void OnEnable()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    public abstract class WeaponComponent<T> : WeaponComponent where T : WeaponComponentData
    {
        protected T data;

        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.GetData<T>();
        }
    }
}
