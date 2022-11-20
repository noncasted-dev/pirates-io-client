using Cysharp.Threading.Tasks;
using GamePlay.Common.Paths;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Local.Services.Abstract;
using UnityEngine;

namespace GamePlay.Services.Wallets.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ServicePrefix + "Wallet",
        menuName = GamePlayAssetsPaths.Wallet + "Service")]
    public class WalletAsset : LocalServiceAsset
    {
        [SerializeField] private Wallet _prefab;

        public override async UniTask Create(
            IServiceBinder serviceBinder,
            ICallbacksRegister callbacksRegister,
            ISceneLoader sceneLoader)
        {
            var wallet = Instantiate(_prefab);
            wallet.name = "Wallet";

            serviceBinder.RegisterComponent(wallet)
                .As<IWallet>()
                .As<IWalletPresenter>();
            serviceBinder.AddToModules(wallet);
        }
    }
}