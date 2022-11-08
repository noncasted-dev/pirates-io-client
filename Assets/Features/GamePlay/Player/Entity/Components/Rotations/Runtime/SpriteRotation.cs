using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using Global.Services.Updaters.Runtime.Abstract;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    public class SpriteRotation : ISpriteRotation, IUpdatable
    {
        public SpriteRotation(
            ISpriteFlipper spriteFlipper,
            IRotation rotation,
            IUpdater updater)
        {
            _spriteFlipper = spriteFlipper;
            _rotation = rotation;
            _updater = updater;
        }

        private readonly IRotation _rotation;

        private readonly ISpriteFlipper _spriteFlipper;
        private readonly IUpdater _updater;

        public void ResetRotation()
        {
            _spriteFlipper.ResetRotation();
        }

        public void RotateX(bool rotateSubSprites)
        {
            if (_rotation.Angle is > 90f and < 270f)
                _spriteFlipper.SetFlipX(true, rotateSubSprites);
            else
                _spriteFlipper.SetFlipX(false, rotateSubSprites);
        }

        public void RotateY(bool rotateSubSprites)
        {
            if (_rotation.Angle is > 90f and < 270f)
                _spriteFlipper.SetFlipX(true, rotateSubSprites);
            else
                _spriteFlipper.SetFlipX(false, rotateSubSprites);
        }

        public void Start()
        {
            _updater.Add(this);
        }

        public void Stop()
        {
            _updater.Remove(this);
        }

        public void OnUpdate(float delta)
        {
            if (_rotation.Angle is > 90f and < 270f)
                _spriteFlipper.SetFlipX(true, true);
            else
                _spriteFlipper.SetFlipX(false, true);
        }
    }
}