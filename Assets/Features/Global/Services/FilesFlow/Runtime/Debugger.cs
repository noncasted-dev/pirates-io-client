#region

using System.Runtime.InteropServices;
using UnityEngine;

#endregion

namespace Global.Services.FilesFlow.Runtime
{
    public static class Debugger
    {
        [DllImport("__Internal")]
        private static extern void WindowAlert(string _message);

        public static void Log(string _message)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                WindowAlert(_message);
            else
                Debug.Log(_message);
        }
    }
}