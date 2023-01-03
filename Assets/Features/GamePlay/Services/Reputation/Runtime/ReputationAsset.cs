using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.Reputation.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Reputation",
        menuName = GamePlayAssetsPaths.Reputation + "Service")]
    public class ReputationAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private Reputation _prefab;

        public override async UniTask Create(
            IDependencyRegister builder, 
            ILocalServiceBinder serviceBinder,
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var reputation = Instantiate(_prefab);
            reputation.name = "Reputation";

            builder.RegisterComponent(reputation)
                .As<IReputation>()
                .As<IReputationPresenter>();

            serviceBinder.AddToModules(reputation);
        }
    }
}