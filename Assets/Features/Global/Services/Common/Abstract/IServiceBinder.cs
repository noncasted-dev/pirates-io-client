using UnityEngine;

namespace Global.Services.Common.Abstract
{
    public interface IServiceBinder
    {
        void AddToModules(MonoBehaviour service);
        void ListenCallbacks(object service);
    }
}