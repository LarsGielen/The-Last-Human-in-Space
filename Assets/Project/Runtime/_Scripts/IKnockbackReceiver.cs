using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public interface IKnockbackReceiver
    {
        public void AddKnockback(Vector2 direction, float force);
    }
}
