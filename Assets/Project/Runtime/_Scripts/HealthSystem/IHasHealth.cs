
namespace Project
{
    public interface IHasHealth
    {
        void Damage(float damageAmount);
        void Heal(float healAmount);
        void Die();
    }
}