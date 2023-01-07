using Common.DiContainer.Abstract;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Weapons.Cannon.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Weapons.Cannon.Views.Transforms;
using UnityEngine;

namespace GamePlay.Player.Entity.Weapons.Cannon.Views.Bootstrap
{
    [DisallowMultipleComponent]
    public class CannonViewsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private CannonSprite _sprite;
        [SerializeField] private CannonTransform _transform;

        public void OnBuild(IDependencyRegister builder)
        {
            builder.RegisterComponent(_sprite).As<ICannonSprite>();
            builder.RegisterComponent(_transform).As<ICannonTransform>();
        }
    }
}