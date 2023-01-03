using UnityEngine;

namespace Common.Local.Services.Abstract
{
    public interface ILocalServiceBinder
    {
        void AddToModules(MonoBehaviour service);
    }
}