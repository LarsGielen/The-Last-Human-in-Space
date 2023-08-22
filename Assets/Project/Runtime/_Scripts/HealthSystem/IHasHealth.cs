using System;

namespace Project
{
    public interface IHasHealth
    {
        event Action<float> OnHealthChanged;
        public float MaxHealth { get; set; }
    }
}
