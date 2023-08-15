using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Explosive : MonoBehaviour, IWeapon
    {
        [SerializeField] private float damage;
        [SerializeField] private float radius;

        [Tooltip("distance in meters")]
        [SerializeField] private float knockBackForce;

        private IWeaponEffect[] effects;

        private void Start()
        {
            effects = new IWeaponEffect[]
            {
                new ExplosiveWeaponEffect(damage, radius, transform.position),
                new KnockBackWeaponEffect(radius, knockBackForce, transform.position)
            };
        }

        public void Trigger()
        {
            foreach (var effect in effects) effect.Trigger();
        }
    }
}
