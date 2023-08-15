using UnityEngine;

namespace Project
{
    public class ExplosiveWeaponEffect : IWeaponEffect
    {
        private float damage;
        private float radius;
        private Vector3 origin;

        public ExplosiveWeaponEffect(float damage, float radius, Vector3 origin)
        {
            this.radius = radius;
            this.damage = damage;
            this.origin = origin;
        }

        public void Trigger()
        {
            Collider[] colliders = Physics.OverlapSphere(origin, radius);

            foreach (var collider in colliders)
            {
                IHasHealth ob;
                if (collider.transform.gameObject.TryGetComponent(out ob))
                {
                    ob.Damage(damage);
                }
            }
        }
    }
}
