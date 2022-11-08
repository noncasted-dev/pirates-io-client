using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Weapons.Bow.Components.Bootstrap
{
    [DisallowMultipleComponent]
    public class BowComponentsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private PlayerComponentAsset[] _assets;

        public void OnBuild(IContainerBuilder builder)
        {
            foreach (var asset in _assets)
                asset.Register(builder);
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            foreach (var asset in _assets)
                asset.Resolve(resolver, callbackRegister);
        }
    }
}