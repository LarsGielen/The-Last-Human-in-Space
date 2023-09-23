using UnityEngine;

namespace Project.Entity.CoreSystem
{
    [RequireComponent(typeof(Core))]
    public abstract class CoreComponent : MonoBehaviour
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = GetComponent<Core>();

            core.AddComponent(this);
        }
    }
}
