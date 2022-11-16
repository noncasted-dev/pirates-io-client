#region

using Global.Services.CameraUtilities.Logs;
using Global.Services.CurrentCameras.Runtime;
using UnityEngine;
using VContainer;

#endregion

namespace Global.Services.CameraUtilities.Runtime
{
    public class CameraUtils : MonoBehaviour, ICameraUtils
    {
        [Inject]
        private void Construct(ICurrentCamera currentCamera, CameraUtilsLogger logger)
        {
            _currentCamera = currentCamera;
            _logger = logger;
        }

        private ICurrentCamera _currentCamera;
        private CameraUtilsLogger _logger;

        public Vector2 ScreenToWorld(Vector2 screen)
        {
            if (_currentCamera.Current == null)
            {
                _logger.OnNoCameraError();
                return Vector2.zero;
            }

            var world = (Vector2)_currentCamera.Current.ScreenToWorldPoint(screen);

            _logger.OnScreenToWorld(screen, world);

            return world;
        }
    }
}