namespace GamePlay.Player.Entity.Components.InertialMovements.Runtime
{
    public interface ISpeedCalculator
    {
        float GetSpeed();
        void OnShallowEntered();
        void OnShallowExited();
    }
}