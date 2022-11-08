using System;
using GamePlay.Player.Entity.Views.Animators.Runtime;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime
{
    public class RangeAttackAnimatorCallbackBridge : IAnimationEndCallback
    {
        public event Action Played;
        public event Action ShootReady;

        public void OnShootReady()
        {
            ShootReady?.Invoke();
        }

        public void OnPlayed()
        {
            Played?.Invoke();
        }
    }
}