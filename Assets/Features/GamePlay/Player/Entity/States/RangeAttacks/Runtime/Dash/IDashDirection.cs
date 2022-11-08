using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash
{
    public interface IDashDirection
    {
        Vector2 Current { get; }

        void OnDirectionInput(Vector2 direction);
        void OnDirectionInputCanceled();
        void OnStarted();
        void OnStopped();
        void Evaluate(float delta);
    }
}