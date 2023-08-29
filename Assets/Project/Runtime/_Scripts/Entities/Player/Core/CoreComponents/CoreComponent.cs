using UnityEngine;

namespace Project.Entity.Player.Core
{
    [RequireComponent(typeof(PlayerCore))]
    public abstract class CoreComponent : MonoBehaviour
    {
        protected PlayerCore core;

        protected virtual void Awake()
        {
            core = GetComponent<PlayerCore>();

            core.AddComponent(this);
        }
    }
}
