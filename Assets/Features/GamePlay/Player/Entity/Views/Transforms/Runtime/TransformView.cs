using Common.Structs;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.Transforms.Logs;
using UnityEngine;
using ILogger = Global.Services.Loggers.Runtime.ILogger;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public class TransformView : MonoBehaviour, IAwakeCallback
    {
        [SerializeField] private TransformLogSettings _logSettings;
        private TransformLogger _logger;

        private Transform _transform;

        public Vector2 Position
        {
            get
            {
                var position = (Vector2)_transform.position;
                _logger.OnPositionUsed(position);
                return position;
            }
        }

        public float Rotation => _transform.rotation.eulerAngles.z;
        public Horizontal Look => AngleUtils.ToHorizontal(Rotation);

        public void OnAwake()
        {
            _transform = transform;
        }

        protected void CreateLogger(ILogger logger)
        {
            _logger = new TransformLogger(logger, _logSettings);
        }

        public void SetPosition(Vector2 position)
        {
            _transform.position = position;

            _logger.OnPositionSet(position);
        }

        public void SetRotation(float angle)
        {
            _transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            _logger.OnRotationSet(angle);
        }
    }
}