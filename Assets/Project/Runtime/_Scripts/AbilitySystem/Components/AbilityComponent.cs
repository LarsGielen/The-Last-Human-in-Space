using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Project.AbilitySystem.Components
{
    public abstract class AbilityComponent : MonoBehaviour
    {
        protected Ability ability;

        private bool usingAbility;

        protected virtual void Awake() => ability = GetComponent<Ability>();

        protected virtual void Start()
        {
            // event not in OnEnable / OnDisable to avoid sequencing issues 
            ability.OnEnter += Enter;
            ability.OnExit += Exit;
        }

        protected virtual void OnDestroy()
        {
            ability.OnEnter -= Enter;
            ability.OnExit -= Exit;
        }

        protected virtual void Update() { if (usingAbility) AbilityUpdate(); }

        /// <summary>
        /// Gets called when generating the component (Switching of ability)
        /// </summary>
        public virtual void Init() { }

        /// <summary>
        /// Gets called upon starting the ability
        /// </summary>
        protected virtual void Enter() => usingAbility = true;

        /// <summary>
        /// Gets called every Unity Update between Enter() and Exit()
        /// </summary>
        protected virtual void AbilityUpdate() { }

        /// <summary>
        /// Gets called upon stoping the ability
        /// </summary>
        protected virtual void Exit() => usingAbility = false;
    }

    public abstract class AbilityComponent<T> : AbilityComponent where T : AbilityComponentData
    {
        protected T data;

        public override void Init()
        {
            base.Init();

            data = ability.Data.GetData<T>();
        }
    }
}
