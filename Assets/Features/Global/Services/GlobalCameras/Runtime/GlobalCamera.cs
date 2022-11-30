using Global.Services.Common.Abstract;
using Global.Services.GlobalCameras.Logs;
using UnityEngine;
using VContainer;

namespace Global.Services.GlobalCameras.Runtime
{
    [DisallowMultipleComponent]
    public class GlobalCamera : MonoBehaviour, IGlobalCamera, IGlobalAwakeListener
    {
        [Inject]
        private void Construct(GlobalCameraLogger logger)
        {
            _logger = logger;
        }

        private Camera _camera;
        private GlobalCameraLogger _logger;

        public void OnAwake()
        {
            _camera = GetComponent<Camera>();
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

            _logger.OnListenerEnabled();
        }

        public void DisableListener()
        {

            _logger.OnListenerDisabled();
        }
    }
}