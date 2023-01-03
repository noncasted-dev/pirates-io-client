using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Runtime.Abstract;
using UnityEngine;

namespace Common.Local.Services.Abstract
{
    public abstract class LocalServiceAsset : ScriptableObject
    {
        public abstract UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks);
    }
}