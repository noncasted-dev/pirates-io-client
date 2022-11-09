using Cysharp.Threading.Tasks;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
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
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var loop = Instantiate(_prefab);
            loop.name = "MenuLoop";

            serviceBinder.RegisterComponent(loop);

            serviceBinder.AddToModules(loop);
            callbacksRegister.ListenFlowCallbacks(loop);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<MenuLoop>();
        }
    }
}