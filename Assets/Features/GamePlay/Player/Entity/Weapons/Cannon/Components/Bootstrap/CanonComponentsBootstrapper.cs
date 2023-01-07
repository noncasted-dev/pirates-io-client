using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Components.Abstract;
using GamePlay.Player.Entity.Setup.Bootstrap;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Bootstrap
{
    [DisallowMultipleComponent]
    public class CanonComponentsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private PlayerComponentAsset[] _assets;

        public void OnBuild(IDependencyRegister builder)
        {
            foreach (var asset in _assets)
                asset.Register(builder);
        }
    }
}