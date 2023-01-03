using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Global.Services.UiStateMachines.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace GamePlay.Services.Maps.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Map", menuName = GamePlayAssetsPaths.Map + "Service")]
    public class MapAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private AssetReference _mapScene;
        [SerializeField] [Indent] private UiConstraints _constraints;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var sceneData = new TypedSceneLoadData<Map>(_mapScene);
            var mapScene = await sceneLoader.Load(sceneData);
            var map = mapScene.Searched;
            
            builder.RegisterComponent(map)
                .WithParameter(_constraints)
                .As<IMap>()
                .AsSelf()
                .AsCallbackListener();

            builder.RegisterComponent(map.Mover)
                .AsSelfResolvable();
        }
    }
}