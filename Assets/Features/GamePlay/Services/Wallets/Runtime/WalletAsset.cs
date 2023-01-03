using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.Wallets.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Wallet",
        menuName = GamePlayAssetsPaths.Wallet + "Service")]
    public class WalletAsset : LocalServiceAsset
    {
        [SerializeField] [Indent] private Wallet _prefab;

        public override async UniTask Create(
            IDependencyRegister builder, 
            ILocalServiceBinder serviceBinder, 
            ISceneLoader sceneLoader,
            ILocalCallbacks callbacks)
        {
            var wallet = Instantiate(_prefab);
            wallet.name = "Wallet";

            builder.RegisterComponent(wallet)
                .As<IWallet>()
                .As<IWalletPresenter>();
            
            serviceBinder.AddToModules(wallet);
        }
    }
}