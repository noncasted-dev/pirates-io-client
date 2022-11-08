namespace GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract
{
    public interface IAnimatorRotation
    {
        void Rotate();
        void Rotate(float angle);
        void Start();
        void Stop();
    }
}