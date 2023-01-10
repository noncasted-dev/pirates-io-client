using GamePlay.Player.Entity.Components.InertialMovements.Runtime;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    public class SpriteRotation : ISpriteRotation, IUpdatable
    {
        public SpriteRotation(
            ISpriteFlipper spriteFlipper,
            IRigidBody rigidBody,
            IUpdater updater)
        {
            _spriteFlipper = spriteFlipper;
            _rigidBody = rigidBody;
            _updater = updater;
        }

        private readonly IInertialMovement _inertialMovement;

        private readonly IRotation _rotation;

        private readonly ISpriteFlipper _spriteFlipper;
        private readonly IRigidBody _rigidBody;
        private readonly IUpdater _updater;

        private Vector2 _previousPosition;

        public void ResetRotation()
        {
            _spriteFlipper.ResetRotation();
        }

        public void RotateX(bool rotateSubSprites)
        {
            var direction = _rigidBody.Position - _previousPosition;
            
            switch (direction.x)
            {
                case > 0f:
                    _spriteFlipper.SetFlipX(false, rotateSubSprites);
                    break;
                case < 0f:
                    _spriteFlipper.SetFlipX(true, rotateSubSprites);
                    break;
            }
            
            _previousPosition = _rigidBody.Position;
        }

        public void Start()
        {
            _previousPosition = _rigidBody.Position;
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