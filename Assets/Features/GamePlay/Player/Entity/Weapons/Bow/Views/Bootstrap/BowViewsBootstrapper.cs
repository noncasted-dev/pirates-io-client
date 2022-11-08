using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Weapons.Bow.Views.Animators.Runtime;
using GamePlay.Player.Entity.Weapons.Bow.Views.ShootPoint;
using GamePlay.Player.Entity.Weapons.Bow.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Weapons.Bow.Views.Transforms;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Weapons.Bow.Views.Bootstrap
{
    [DisallowMultipleComponent]
    public class BowViewsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private BowAnimator _animator;
        [SerializeField] private BowSprite _sprite;
        [SerializeField] private BowTransform _transform;
        [SerializeField] private BowShootPoint _shootPoint;

        public void OnBuild(IContainerBuilder builder)
        {
            builder.RegisterComponent(_animator).AsSelf();
            builder.RegisterComponent(_sprite).As<IBowSprite>();
            builder.RegisterComponent(_transform).As<IBowTransform>();
            builder.RegisterComponent(_shootPoint).As<IShootPoint>();
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(_animator);
            callbackRegister.Add(_sprite);
            callbackRegister.Add(_transform);
        }
    }
}