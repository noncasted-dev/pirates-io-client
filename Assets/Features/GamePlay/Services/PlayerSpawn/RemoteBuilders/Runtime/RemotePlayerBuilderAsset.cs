using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "RemotePlayerBuilder",
        menuName = GamePlayAssetsPaths.RemotePlayerBuilder + "Service")]
    public class RemotePlayerBuilderAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private AssetReference _poolScene;
        [SerializeField] [Indent] private RemotePlayerBuilder _prefab;
        [SerializeField] [Indent] private RemoteBuilderConfigAsset _config;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var remoteBuilder = Instantiate(_prefab);
            remoteBuilder.name = "RemotePlayerBuilder";

            var pool = remoteBuilder.GetComponent<RemoteViewsPool>();

            builder.RegisterComponent(remoteBuilder)
                .WithParameter(_config)
                .AsCallbackListener();

            builder.RegisterComponent(pool)
                .AsCallbackListener();

            var scene = await sceneLoader.Load(new EmptySceneLoadData(_poolScene));
            SceneManager.MoveGameObjectToScene(remoteBuilder.gameObject, scene.Instance.Scene);

            pool.OnSceneLoaded(scene.Instance.Scene);
        }
    }
}