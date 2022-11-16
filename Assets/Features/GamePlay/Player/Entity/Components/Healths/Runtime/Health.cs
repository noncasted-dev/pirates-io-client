using GamePlay.Player.Entity.Components.Healths.Logs;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public class Health : IHealth
    {
        public Health(HealthLogger logger)
        {
            _logger = logger;
        }

        private readonly HealthLogger _logger;

        private readonly int _max;

        private int _amount;

        public bool IsAlive => _amount > 0;

        public void Respawn(int health)
        {
            _logger.OnRespawned(health);

            _amount = health;
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
        }
    }
}