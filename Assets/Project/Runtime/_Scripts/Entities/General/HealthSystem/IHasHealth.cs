using System;

namespace Project.Entity
{
    public interface IHasHealth
    {
        event Action<float> OnHealthChanged;
        public float MaxHealth { get; }
    }
}
