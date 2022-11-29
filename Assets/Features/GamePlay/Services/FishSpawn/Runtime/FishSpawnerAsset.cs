using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.FishSpawn.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "FishSpawner",
        menuName = GamePlayAssetsPaths.FishSpawner + "Service")]
    public class FishSpawnerAsset : LocalServiceAsset
    {
        [SerializeField] private FishSpawner _prefab;

        public override async UniTask Create(IServiceBinder serviceBinder, ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var spawner = Instantiate(_prefab);
            spawner.name = "FishSpawner";

            serviceBinder.RegisterComponent(spawner)
                .AsSelf();

            serviceBinder.AddToModules(spawner);
            callbacksRegister.ListenLoopCallbacks(spawner);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<FishSpawner>();
        }
    }
}