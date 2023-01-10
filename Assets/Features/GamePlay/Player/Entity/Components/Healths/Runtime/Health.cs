using Common.VFX;
using GamePlay.Player.Entity.Components.Healths.Logs;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Updaters.Runtime.Abstract;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public class Health : IHealth, IPlayerSwitchListener, IUpdatable
    {
        public Health(
            HealthLogger logger,
            IUpdater updater,
            FireController fireController,
            IPlayerEventSender eventSender,
            IBodyTransform transform)
        {
            _eventSender = eventSender;
            _transform = transform;
            _logger = logger;
            _updater = updater;
            _fireController = fireController;
        }

        private const float _tickDuration = 5f;
        private readonly FireController _fireController;

        private readonly HealthLogger _logger;
        private readonly IBodyTransform _transform;
        private readonly IUpdater _updater;
        private int _amount;
        private readonly IPlayerEventSender _eventSender;

        private int _max;
        private int _regenerationInTick;

        private float _tickTime;

        public int Max => _max;
        public int Amount => _amount;
        public bool IsAlive => _amount > 0;

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
            _fireController.SetFireForce(_amount / (float)_max);

            Msg.Publish(new HealthChangeEvent(_amount, _max, _transform.GameObject));

            _eventSender.ReplicateEvent(new HealthChangeNetworkEvent
            {
                Current = _amount,
                Max = _max
            });
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

            _fireController.SetFireForce(_amount / (float)_max);

            _logger.OnHealed(add, _amount);

            _eventSender.ReplicateEvent(new HealthChangeNetworkEvent
            {
                Current = _amount,
                Max = _max
            });

            Msg.Publish(new HealthChangeEvent(_amount, _max, _transform.GameObject));
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

            _fireController.SetFireForce(_amount / (float)_max);

            Msg.Publish(new HealthChangeEvent(_amount, _max, _transform.GameObject));

            _eventSender.ReplicateEvent(new HealthChangeNetworkEvent
            {
                Current = _amount,
                Max = _max
            });
        }

        public void OnEnabled()
        {
            _updater.Add(this);
            _fireController.SetFireForce(1f);
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
    }
}