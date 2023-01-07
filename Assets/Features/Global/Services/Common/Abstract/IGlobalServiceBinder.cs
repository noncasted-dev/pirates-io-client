using UnityEngine;

namespace Global.Services.Common.Abstract
{
    public interface IGlobalServiceBinder
    {
        void AddToModules(MonoBehaviour service);
    }
}