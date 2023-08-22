using System;

namespace Project
{
    public interface IHasHealth
    {
        event Action<float> OnHealthChanged;
        float GetMaxHealth();
    }
}
