using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Bootstrap;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Bootstrap
{
    [DisallowMultipleComponent]
    public class PlayerComponentsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private PlayerBootstrapConfig _assets;

        public void OnBuild(IDependencyRegister builder)
        {
            var assets = _assets.GetAssets();

            foreach (var asset in assets)
                asset.Register(builder);
        }
    }
}