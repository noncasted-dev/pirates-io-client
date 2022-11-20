using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

namespace GamePlay.Services.Reputation.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Reputation",
        menuName = GamePlayAssetsPaths.Reputation + "Service")]
    public class ReputationAsset : LocalServiceAsset
    {
        [SerializeField] private Reputation _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var reputation = Instantiate(_prefab);
            reputation.name = "Reputation";

            serviceBinder.RegisterComponent(reputation)
                .As<IReputation>()
                .As<IReputationPresenter>();

            serviceBinder.AddToModules(reputation);
        }

        
    }
}