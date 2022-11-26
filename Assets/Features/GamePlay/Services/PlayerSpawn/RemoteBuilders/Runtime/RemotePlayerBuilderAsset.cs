using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "RemotePlayerBuilder",
        menuName = GamePlayAssetsPaths.RemotePlayerBuilder + "Service")]
    public class RemotePlayerBuilderAsset : LocalServiceAsset
    {
        [SerializeField] private AssetReference _poolScene;
        [SerializeField] private RemotePlayerBuilder _prefab;
        [SerializeField]  private RemoteBuilderConfigAsset _config;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var builder = Instantiate(_prefab);
            builder.name = "RemotePlayerBuilder";

            var pool = builder.GetComponent<RemoteViewsPool>();

            serviceBinder.RegisterComponent(builder)
                .WithParameter(_config)
                .AsSelf();

            serviceBinder.RegisterComponent(pool)
                .AsSelf();

            var scene = await sceneLoader.Load(new EmptySceneLoadData(_poolScene));
            SceneManager.MoveGameObjectToScene(builder.gameObject, scene.Instance.Scene);

            pool.OnSceneLoaded(scene.Instance.Scene);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            callbacksRegister.ListenLoopCallbacks(resolver.Resolve<RemotePlayerBuilder>());

            var pool = resolver.Resolve<RemoteViewsPool>();
            callbacksRegister.ListenContainerCallbacks(pool);
            callbacksRegister.ListenLoopCallbacks(pool);
        }
    }
}