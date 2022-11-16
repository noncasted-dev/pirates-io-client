#region

using UnityEngine;
using VContainer;

#endregion

namespace Local.Services.Abstract
{
    public interface IServiceBinder
    {
        void AddToModules(MonoBehaviour service);
        RegistrationBuilder Register<T>();
        ComponentRegistration RegisterComponent<T>(T component) where T : MonoBehaviour;
    }
}