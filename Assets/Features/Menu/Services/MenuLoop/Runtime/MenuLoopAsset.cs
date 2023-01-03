using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Menu.Common;
using UnityEngine;
using VContainer;

namespace Menu.Services.MenuLoop.Runtime
{
    [CreateAssetMenu(fileName = MenuAssetsPaths.ServicePrefix + "MenuLoop",
        menuName = MenuAssetsPaths.Loop)]
    public class MenuLoopAsset : LocalServiceAsset
    {
        [SerializeField] private MenuLoop _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var loop = Instantiate(_prefab);
            loop.name = "MenuLoop";

            builder.RegisterComponent(loop)
                .AsCallbackListener();

            serviceBinder.AddToModules(loop);
        }
    }
}