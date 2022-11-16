#region

using UnityEngine;

#endregion

namespace Global.Services.GlobalCameras.Runtime
{
    public interface IGlobalCamera
    {
        Camera Camera { get; }
        void Enable();
        void Disable();
        void EnableListener();
        void DisableListener();
    }
}