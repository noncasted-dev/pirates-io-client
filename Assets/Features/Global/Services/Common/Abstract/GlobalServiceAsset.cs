using Common.DiContainer.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.Common.Abstract.Scenes;
using UnityEngine;

namespace Global.Services.Common.Abstract
{
    public abstract class GlobalServiceAsset : ScriptableObject
    {
        public abstract UniTask Create(
            IDependencyRegister builder,
            IGlobalServiceBinder serviceBinder,
            IGlobalSceneLoader sceneLoader,
            IGlobalCallbacks callbacks);
    }
}