using System;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    [DisallowMultipleComponent]
    public class RespawnAnimatorCallbacks : MonoBehaviour, IAnimationEndCallback
    {
        public event Action Played;

        public void OnRespawnPlayed()
        {
            Played?.Invoke();
        }
    }
}