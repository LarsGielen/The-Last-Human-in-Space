using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class KnockBackWeaponEffect : IWeaponEffect
    {
        private float radius;
        private float force;
        private Vector3 origin;

        public KnockBackWeaponEffect(float radius, float force, Vector3 origin)
        {
            this.radius = radius;
            this.force = force;
            this.origin = origin;
        }

        public void Trigger()
        {
            Collider[] colliders = Physics.OverlapSphere(origin, radius);

            foreach (var collider in colliders)
            {
                IKnockbackReceiver ob;
                if (collider.TryGetComponent(out ob))
                {
                    Vector3 direction = collider.transform.position - origin;

                    ob.AddKnockback(new Vector2(direction.x, direction.z), Mathf.InverseLerp(radius, 0, direction.magnitude) * force);
                }
            }
        }
    }
}
