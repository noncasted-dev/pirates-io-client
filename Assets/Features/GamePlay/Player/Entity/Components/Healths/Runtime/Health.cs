using GamePlay.Player.Entity.Components.Healths.Logs;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using Global.Services.Updaters.Runtime.Abstract;
using UniRx;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public class Health : IHealth, ISwitchCallbacks, IUpdatable
    {
        public Health(HealthLogger logger, IUpdater updater)
        {
            _logger = logger;
            _updater = updater;
        }


        private const float _tickDuration = 5f;

        private readonly HealthLogger _logger;
        private readonly IUpdater _updater;

        private int _max;
        private int _amount;

        private float _tickTime;
        private int _regenerationInTick;

        public int Max => _max;
        public int Amount => _amount;
        public bool IsAlive => _amount > 0;

        public void OnEnabled()
        {
            _updater.Add(this);
        }

        public void OnDisabled()
        {
            _updater.Remove(this);
        }
        
        public void OnUpdate(float delta)
        {
            _tickTime += delta;
            
            if (_tickTime < _tickDuration)
                return;

            _tickTime = 0f;
            
            if (_amount == _max)
                return;
            
            Heal(_regenerationInTick);
        }
        
        public void SetMaxHealth(int maxHealth, int regenerationInTick)
        {
            _regenerationInTick = regenerationInTick;
            _max = maxHealth;
            _amount = maxHealth;
        }

        public void Respawn()
        {
            _logger.OnRespawned(_max);
            
            _amount = _max;

            MessageBroker.Default.Publish(new HealthChangeEvent(_amount, _max));
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

            MessageBroker.Default.Publish(new HealthChangeEvent(_amount, _max));
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

            MessageBroker.Default.Publish(new HealthChangeEvent(_amount, _max));
        }
    }
}