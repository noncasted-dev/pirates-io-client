using GamePlay.Player.Entity.Components.Healths.Logs;
using UniRx;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public class Health : IHealth
    {
        public Health(HealthLogger logger)
        {
            _logger = logger;
        }

        private readonly HealthLogger _logger;

        private int _max;
        private int _amount;

        public int Max => _max;
        public int Amount => _amount;
        public bool IsAlive => _amount > 0;

        public void SetMaxHealth(int maxHealth)
        {
            _max = maxHealth;
            _amount = maxHealth;
        }

        public void Respawn()
        {
            _logger.OnRespawned(_max);

            _amount = _max;
            
            MessageBroker.Default.Publish(new HealthChangedEvent(_amount, _max));
        }

        public void Heal(int add)
        {
            if (add < 0)
            {
                _logger.OnHealValueException(add);
                add = 0;
            }

            _amount += add;

            if (_amount > _max)
                _amount = _max;

            _logger.OnHealed(add, _amount);
            
            MessageBroker.Default.Publish(new HealthChangedEvent(_amount, _max));
        }

        public void ApplyDamage(int damage)
        {
            if (damage < 0)
            {
                _logger.OnDamageValueException(damage);
                damage = 0;
            }

            _amount -= damage;

            _logger.OnDamaged(damage, _amount);
            
            MessageBroker.Default.Publish(new HealthChangedEvent(_amount, _max));
        }
    }
}