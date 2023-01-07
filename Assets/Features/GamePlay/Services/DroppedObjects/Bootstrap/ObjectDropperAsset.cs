using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.DroppedObjects.Network.Runtime;
using GamePlay.Services.DroppedObjects.Pool.Runtime;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GamePlay.Services.DroppedObjects.Bootstrap
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "ObjectsDropper",
        menuName = GamePlayAssetsPaths.ObjectsDropper + "Service")]
    public class ObjectDropperAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private AssetReference _poolScene;
        [SerializeField] [Indent] private ObjectDropperConfigAsset _config;
        [SerializeField] [Indent] private DropPoolBootstrapper _poolPrefab;
        [SerializeField] [Indent] private DroppedObjectsPresenter _dropperPrefab;

        public override async UniTask Create(
            IDependencyRegister builder,
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var pool = Instantiate(_poolPrefab);
            pool.name = "Pool_Drop";

            var dropper = Instantiate(_dropperPrefab);
            dropper.name = "ObjectDropper";

            var network = dropper.GetComponent<ObjectDropNetworker>();

            builder.Register<DropPoolProvider>()
                .As<IDropPoolProvider>()
                .WithParameter<IPoolProvider>(pool.Handler)
                .AsCallbackListener();

            builder.RegisterComponent(dropper)
                .As<IDroppedObjectsPresenter>()
                .WithParameter(_config)
                .AsCallbackListener()
                .AsSelfResolvable();

            builder.RegisterComponent(network)
                .As<INetworkObjectDropSender>()
                .As<INetworkObjectDropReceiver>();

            serviceBinder.AddToModules(dropper);

            var scene = await sceneLoader.Load(new EmptySceneLoadData(_poolScene));
            SceneManager.MoveGameObjectToScene(pool.gameObject, scene.Instance.Scene);

            callbacks.Listen(pool);

            pool.OnSceneLoaded(scene.Instance.Scene);
        }
    }
}