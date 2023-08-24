using UnityEngine;

namespace Project.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        protected bool isAttacking;

        protected virtual void Awake() => weapon = GetComponent<Weapon>();

        protected virtual void Start()
        {
            // event not in OnEnable / OnDisable to avoid sequencing issues 
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnDestroy()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }

        public virtual void Init() { }

        protected virtual void HandleEnter() => isAttacking = true;

        protected virtual void HandleExit() => isAttacking = false;
    }

    public abstract class WeaponComponent<T> : WeaponComponent where T : WeaponComponentData
    {
        protected T data;

        public override void Init()
        {
            base.Init();

            data = weapon.Data.GetData<T>();
        }
    }
}
