using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace Local.Services.Abstract
{
    public abstract class LocalServiceAsset : ScriptableObject, ILocalService
    {
        public abstract UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader);

        public virtual void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
        }
    }
}