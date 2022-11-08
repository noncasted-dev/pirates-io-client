using Global.Services.Common.Abstract;
using Global.Services.GlobalCameras.Logs;
using UnityEngine;
using VContainer;

namespace Global.Services.GlobalCameras.Runtime
{
    [DisallowMultipleComponent]
    public class GlobalCamera : MonoBehaviour, IGlobalCamera, IGlobalServiceAwakeListener
    {
        [Inject]
        private void Construct(GlobalCameraLogger logger)
        {
            _logger = logger;
        }

        private Camera _camera;
        private AudioListener _listener;
        private GlobalCameraLogger _logger;

        private void Update()
        {
            if (Camera.allCamerasCount > 1)
                DisableListener();
            else
                EnableListener();
        }

        public Camera Camera => _camera;

        public void Enable()
        {
            gameObject.SetActive(true);

            _logger.OnEnabled();
        }

        public void Disable()
        {
            gameObject.SetActive(false);

            _logger.OnDisabled();
        }

        public void EnableListener()
        {
            _listener.enabled = true;

            _logger.OnListenerEnabled();
        }

        public void DisableListener()
        {
            _listener.enabled = false;

            _logger.OnListenerDisabled();
        }

        public void OnAwake()
        {
            _listener = GetComponent<AudioListener>();
            _camera = GetComponent<Camera>();
        }
    }
}