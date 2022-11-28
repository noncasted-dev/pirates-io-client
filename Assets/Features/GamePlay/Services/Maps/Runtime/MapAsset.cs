using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Features.GamePlay.Services.Maps.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Map", menuName = GamePlayAssetsPaths.Map + "Service")]
    public class MapAsset : LocalServiceAsset
    {
        [SerializeField] private AssetReference _mapScene;
        [SerializeField] private UiConstraints _constraints;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var sceneData = new TypedSceneLoadData<Map>(_mapScene);
            var mapScene = await sceneLoader.Load(sceneData);
            var map = mapScene.Searched;
            
            serviceBinder.RegisterComponent(map)
                .WithParameter(_constraints)
                .As<IMap>()
                .AsSelf();
            
            callbacksRegister.ListenLoopCallbacks(map);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            var map = resolver.Resolve<Map>();
            var mover = map.Mover;

            resolver.Inject(mover);
        }
    }
}