namespace GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract
{
    public interface ISpriteRotation
    {
        void ResetRotation();
        void RotateX(bool rotateSubSprites);
        void Start();
        void Stop();
    }
}