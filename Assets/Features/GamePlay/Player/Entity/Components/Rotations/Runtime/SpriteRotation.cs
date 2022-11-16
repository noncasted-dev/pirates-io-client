using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using Global.Services.Updaters.Runtime.Abstract;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    public class SpriteRotation : ISpriteRotation, IUpdatable
    {
        public SpriteRotation(
            ISpriteFlipper spriteFlipper,
            IInertialMovement inertialMovement,
            IUpdater updater)
        {
            _spriteFlipper = spriteFlipper;
            _inertialMovement = inertialMovement;
            _updater = updater;
        }

        private readonly IInertialMovement _inertialMovement;

        private readonly IRotation _rotation;

        private readonly ISpriteFlipper _spriteFlipper;
        private readonly IUpdater _updater;

        public void ResetRotation()
        {
            _spriteFlipper.ResetRotation();
        }

        public void RotateX(bool rotateSubSprites)
        {
            switch (_inertialMovement.XDirection)
            {
                case > 0f:
                    _spriteFlipper.SetFlipX(false, rotateSubSprites);
                    break;
                case < 0f:
                    _spriteFlipper.SetFlipX(true, rotateSubSprites);
                    break;
            }
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
            RotateX(true);
        }
    }
}