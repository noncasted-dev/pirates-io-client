using System;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public class Sail : ISail, IPlayerSwitchListener, IUpdatable
    {
        public Sail(IUpdater updater)
        {
            _updater = updater;
        }

        private const float _tickDuration = 5f;
        private const float _max = 100f;

        private readonly IUpdater _updater;

        private float _amount = 100f;
        private int _regenerationInTick;

        private float _tickTime;

        public bool IsAlive => _amount > 0;

        public int Strength => Mathf.CeilToInt(_amount);
        public event Action Changed;

        public void Damage(float damage)
        {
            if (damage < 0)
                damage = 0;

            _amount -= damage;

            if (_amount < 20f)
                _amount = 20f;

            Changed?.Invoke();
        }

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

            if (_amount >= _max)
                return;

            Heal(_regenerationInTick);
        }

        public void Heal(int add)
        {
            if (add < 0)
                add = 0;

            _amount += add;

            if (_amount > _max)
                _amount = _max;

            if (_amount < 20f)
                _amount = 20f;

            Changed?.Invoke();
        }
    }
}