#region

using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack
{
    public class AttackDelay
    {
        public AttackDelay(IRangeAttackConfig config)
        {
            _config = config;
        }

        private readonly IRangeAttackConfig _config;

        private float _previous;

        public bool IsAvailable()
        {
            if (_previous + _config.Delay > Time.time)
                return false;

            return true;
        }

        public void OnAttack()
        {
            _previous = Time.time;
        }
    }
}