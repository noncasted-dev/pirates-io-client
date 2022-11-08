using UnityEngine;

namespace Global.Services.ApplicationProxies.Runtime
{
    [DisallowMultipleComponent]
    public class ApplicationProxy : MonoBehaviour, IApplicationFlow
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}