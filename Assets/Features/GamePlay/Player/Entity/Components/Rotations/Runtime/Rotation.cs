using GamePlay.Player.Entity.Components.Rotations.Logs;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.RotationPoint;
using Global.Services.InputViews.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime
{
    public class Rotation : IPreUpdatable, IRotation, ISwitchCallbacks
    {
        public Rotation(
            IInputView input,
            IUpdater updater,
            IRotationPoint point,
            RotationLogger logger)
        {
            _input = input;
            _updater = updater;
            _point = point;
            _logger = logger;
        }

        private readonly IInputView _input;
        private readonly RotationLogger _logger;
        private readonly IRotationPoint _point;
        private readonly IUpdater _updater;

        private float _angle;

        public void OnPreUpdate(float delta = 0f)
        {
            _angle = _input.GetAngleFrom(_point.Position);

            _logger.OnRotationSet(_angle);
        }

        public float Angle
        {
            get
            {
                _logger.OnRotationUsed(_angle);

                return _angle;
            }
        }

        public Quaternion Quaternion => Quaternion.Euler(0f, 0f, _angle);

        public void OnEnabled()
        {
            _updater.Add(this);
        }

        public void OnDisabled()
        {
            _updater.Remove(this);
        }
    }
}