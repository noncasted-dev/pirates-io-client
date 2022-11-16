using UnityEngine;

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    public interface IRun
    {
        bool HasInput { get; }

        void OnInput(Vector2 input);
        void OnCancel();
        void Enter();
    }
}