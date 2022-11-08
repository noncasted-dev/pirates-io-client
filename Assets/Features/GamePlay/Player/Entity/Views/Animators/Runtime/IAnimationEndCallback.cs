using System;

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    public interface IAnimationEndCallback
    {
        event Action Played;
    }
}