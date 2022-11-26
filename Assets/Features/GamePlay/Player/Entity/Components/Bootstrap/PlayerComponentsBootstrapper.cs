using Common.EditableScriptableObjects.Attributes;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Components.Bootstrap
{
    [DisallowMultipleComponent]
    public class PlayerComponentsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField]  private PlayerBootstrapConfig _assets;

        public void OnBuild(IContainerBuilder builder)
        {
            var assets = _assets.GetAssets();

            foreach (var asset in assets)
                asset.Register(builder);
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            var assets = _assets.GetAssets();

            foreach (var asset in assets)
                asset.Resolve(resolver, callbackRegister);
        }
    }
}