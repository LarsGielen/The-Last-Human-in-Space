using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Core
{
    [RequireComponent(typeof(PlayerCore))]
    public class CoreComponent : MonoBehaviour
    {
        protected PlayerCore core;

        protected virtual void Awake()
        {
            core = GetComponent<PlayerCore>();

            core.AddComonponent(this);
        }
    }
}
