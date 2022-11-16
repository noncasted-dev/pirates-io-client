#region

using Global.Services.CurrentCameras.Logs;
using UnityEngine;
using VContainer;

#endregion

namespace Global.Services.CurrentCameras.Runtime
{
    public class CurrentCamera : MonoBehaviour, ICurrentCamera
    {
        [Inject]
        private void Construct(CurrentCameraLogger logger)
        {
            _logger = logger;
        }

        private Camera _current;

        private CurrentCameraLogger _logger;

        public Camera Current
        {
            get
            {
                _logger.OnUsed(_current);

                return _current;
            }
        }

        public void SetCamera(Camera current)
        {
            _current = current;

            _logger.OnSetted(current);
        }
    }
}