using GamePlay.Services.LevelCameras.Logs;
using Global.Services.CurrentCameras.Runtime;
using Global.Services.InputViews.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.LevelCameras.Runtime
{
    public class LevelCamera :
        MonoBehaviour,
        ILevelCamera,
        ILocalAwakeListener,
        ILocalSwitchListener,
        IPostFixedUpdatable
    {
        [Inject]
        private void Construct(
            ICurrentCamera currentCamera,
            IUpdater updater,
            ILevelCameraConfig config,
            IInputView inputView,
            LevelCameraLogger logger)
        {
            _inputView = inputView;
            _config = config;
            _updater = updater;
            _logger = logger;
            _currentCamera = currentCamera;

            _transform = transform;
        }

        private const float _offsetZ = -10f;

        private Camera _camera;
        private ILevelCameraConfig _config;
        private ICurrentCamera _currentCamera;
        private IInputView _inputView;

        private LevelCameraLogger _logger;

        private Transform _target;

        private Transform _transform;
        private IUpdater _updater;

        public Camera Camera => _camera;

        public void StartFollow(Transform target)
        {
            _target = target;

            _logger.OnStartFollow(target.name);
        }

        public void StopFollow()
        {
            if (_target == null)
            {
                _logger.OnStopFollowError();
                return;
            }

            _logger.OnStopFollow(_target.name);

            _target = null;
        }

        public void Teleport(Vector2 target)
        {
            var position = new Vector3(target.x, target.y, _offsetZ);
            _transform.position = position;

            _logger.OnTeleport(position);
        }
 
        public void SetSize(float size)
        {
            _camera.orthographicSize = size;
        }

        public void OnAwake()
        {
            _camera = GetComponent<Camera>();
            _currentCamera.SetCamera(_camera);
        }

        public void OnEnabled()
        {
            _updater.Add(this);
        }

        public void OnDisabled()
        {
            _updater.Remove(this);
        }

        public void OnPostFixedUpdate(float delta)
        {
            if (_target == null)
                return;

            var targetPosition = _target.position;
            var line = _inputView.GetLineFrom(targetPosition);

            var distanceToCursor =
                line.Length - Vector2.Distance(targetPosition, _transform.position);

            var sight = _config.CreateSight(line.Direction, distanceToCursor);

            if (sight.IsOversight == true)
                targetPosition += sight.CreateOversightMove();

            var speed = _config.FollowSpeed * delta;
            var position = Vector3.Lerp(_transform.position, targetPosition, speed);
            position.z = _offsetZ;

            _transform.position = position;
        }
    }
}