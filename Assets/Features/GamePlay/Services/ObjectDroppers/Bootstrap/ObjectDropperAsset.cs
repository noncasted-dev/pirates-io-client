﻿#region

using Common.EditableScriptableObjects.Attributes;
using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.ObjectDroppers.Network.Runtime;
using GamePlay.Services.ObjectDroppers.Pool.Runtime;
using GamePlay.Services.ObjectDroppers.Presenter.Runtime;
using Global.Services.ScenesFlow.Handling.Data;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

#endregion

namespace GamePlay.Services.ObjectDroppers.Bootstrap
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "ObjectsDropper",
        menuName = GamePlayAssetsPaths.ObjectsDropper + "Service")]
    public class ObjectDropperAsset : LocalServiceAsset
    {
        [SerializeField] [EditableObject] private ObjectDropperConfigAsset _config;
        [SerializeField] private AssetReference _poolScene;
        [SerializeField] private DropPoolBootstrapper _poolPrefab;
        [SerializeField] private ObjectDropper _dropperPrefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var pool = Instantiate(_poolPrefab);
            pool.name = "Pool_Drop";

            var dropper = Instantiate(_dropperPrefab);
            dropper.name = "ObjectDropper";

            var network = dropper.GetComponent<ObjectDropNetworker>();

            serviceBinder.Register<DropPoolProvider>()
                .As<IDropPoolProvider>()
                .WithParameter<IPoolProvider>(pool.Handler);

            serviceBinder.RegisterComponent(dropper)
                .As<IObjectDropper>()
                .WithParameter<ObjectDropperConfigAsset>(_config);

            serviceBinder.RegisterComponent(network)
                .As<INetworkObjectDropSender>()
                .As<INetworkObjectDropReceiver>();

            serviceBinder.AddToModules(dropper);

            var scene = await sceneLoader.Load(new EmptySceneLoadData(_poolScene));
            SceneManager.MoveGameObjectToScene(pool.gameObject, scene.Instance.Scene);

            callbacksRegister.ListenLoopCallbacks(pool);
            callbacksRegister.ListenLoopCallbacks(dropper);
            callbacksRegister.ListenContainerCallbacks(pool);

            pool.OnSceneLoaded(scene.Instance.Scene);
        }

        public override void OnResolve(IObjectResolver resolver, ICallbacksRegister callbacksRegister)
        {
            resolver.Resolve<IObjectDropper>();
        }
    }
}