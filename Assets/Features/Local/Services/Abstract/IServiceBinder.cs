using UnityEngine;
using VContainer;

namespace Local.Services.Abstract
{
    public interface IServiceBinder
    {
        void AddToModules(MonoBehaviour service);
        RegistrationBuilder Register<T>();
        ComponentRegistration RegisterComponent<T>(T component) where T : MonoBehaviour;
    }
}