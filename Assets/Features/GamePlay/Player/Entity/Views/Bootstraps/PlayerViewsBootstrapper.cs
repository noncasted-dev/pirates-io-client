using GamePlay.Player.Entity.Network.Local.AreaInteractors.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using GamePlay.Player.Entity.Views.RotationPoint;
using GamePlay.Player.Entity.Views.ShootPoint;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Views.Transforms.Runtime;
using GamePlay.Player.Entity.Views.WeaponsRoots.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Views.Bootstraps
{
    public class PlayerViewsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private PlayerSpriteView _sprite;
        [SerializeField] private PlayerBodyTransform _transform;
        [SerializeField] private PlayerRotationPoint _rotationPoint;
        [SerializeField] private PlayerRigidBody _rigidBody;
        [SerializeField] private WeaponsRoot _weaponsRoot;
        [SerializeField] private AimView _aim;
        [SerializeField] private CannonShootPoint _shootPoint;
        [SerializeField] private PlayerSpriteTransform _spriteTransform;
        [SerializeField] private LocalAreaInteractor _areaInteractor;

        public void OnBuild(IContainerBuilder builder)
        {
            builder.RegisterComponent(_sprite).AsImplementedInterfaces();
            builder.RegisterComponent(_transform).As<IBodyTransform>();
            builder.RegisterComponent(_rotationPoint).AsImplementedInterfaces();
            builder.RegisterComponent(_rigidBody).AsImplementedInterfaces();
            builder.RegisterComponent(_weaponsRoot).AsImplementedInterfaces();
            builder.RegisterComponent(_aim).AsImplementedInterfaces();
            builder.RegisterComponent(_shootPoint).AsImplementedInterfaces();
            builder.RegisterComponent(_spriteTransform).As<ISpriteTransform>();
            builder.RegisterComponent(_areaInteractor);
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(_sprite);
            callbackRegister.Add(_transform);
            callbackRegister.Add(_rotationPoint);
            callbackRegister.Add(_rigidBody);
            callbackRegister.Add(_weaponsRoot);
            callbackRegister.Add(_spriteTransform);
            resolver.Resolve<LocalAreaInteractor>();
        }
    }
}