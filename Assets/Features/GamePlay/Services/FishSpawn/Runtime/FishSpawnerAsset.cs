using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.FishSpawn.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "FishSpawner",
        menuName = GamePlayAssetsPaths.FishSpawner + "Service")]
    public class FishSpawnerAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private FishSpawner _prefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var spawner = Instantiate(_prefab);
            spawner.name = "FishSpawner";

            builder.RegisterComponent(spawner)
                .AsCallbackListener();

            serviceBinder.AddToModules(spawner);
        }
    }
}