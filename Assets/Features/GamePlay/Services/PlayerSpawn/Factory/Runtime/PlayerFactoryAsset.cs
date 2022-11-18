using Common.EditableScriptableObjects.Attributes;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "PlayerFactory",
        menuName = GamePlayAssetsPaths.PlayerFactory + "Service")]
    public class PlayerFactoryAsset : LocalServiceAsset
    {
        [SerializeField] [EditableObject] private PlayerFactoryLogSettings _logSettings;
        [SerializeField] private PlayerFactory _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var factory = Instantiate(_prefab);
            factory.name = "PlayerFactory";

            serviceBinder.Register<PlayerFactoryLogger>()
                .WithParameter(_logSettings);

            serviceBinder.RegisterComponent(factory)
                .As<IPlayerFactory>();

            callbacksRegister.ListenContainerCallbacks(factory);
            serviceBinder.AddToModules(factory);
        }
    }
}