#region

using UnityEngine;

#endregion

namespace Global.Services.CurrentCameras.Runtime
{
    public interface ICurrentCamera
    {
        Camera Current { get; }

        void SetCamera(Camera current);
    }
}