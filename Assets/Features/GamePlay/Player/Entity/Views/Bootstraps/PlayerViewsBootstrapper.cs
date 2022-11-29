using GamePlay.Player.Entity.Network.Local.AreaInteractors.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim;
using GamePlay.Player.Entity.Views.DebugTool;
using GamePlay.Player.Entity.Views.ObjectsCollector.Runtime;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using GamePlay.Player.Entity.Views.RotationPoint;
using GamePlay.Player.Entity.Views.ShipConfig.Runtime;
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
        [SerializeField] private PlayerRotationPoint _rotationPoint;
        [SerializeField] private PlayerRigidBody _rigidBody;
        [SerializeField] private WeaponsRoot _weaponsRoot;
        [SerializeField] private AimView _aim;
        [SerializeField] private CannonShootPoint _shootPoint;
        [SerializeField] private LocalAreaInteractor _areaInteractor;
        [SerializeField] private PlayerObjectsCollector _objectsCollector;
        [SerializeField] private PlayerSpriteView _sprite;
        [SerializeField] private PlayerSpriteTransform _spriteTransform;
        [SerializeField] private PlayerBodyTransform _transform;
        [SerializeField] private PlayerShipConfig _config;
        [SerializeField] private PlayerDebugTool _debug;
        [SerializeField] private FireController _fireController;
        [SerializeField] private PlayerStatsConfig _statsConfig;
        
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
            builder.RegisterComponent(_objectsCollector);
            builder.RegisterComponent(_debug);
            builder.RegisterComponent(_fireController);
            builder.RegisterComponent(_statsConfig);
            
            builder.RegisterComponent(_config)
                .As<IShipConfig>();
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(_sprite);
            callbackRegister.Add(_transform);
            callbackRegister.Add(_rotationPoint);
            callbackRegister.Add(_rigidBody);
            callbackRegister.Add(_weaponsRoot);
            callbackRegister.Add(_spriteTransform);
            callbackRegister.Add(_statsConfig);
            resolver.Resolve<LocalAreaInteractor>();
            resolver.Resolve<PlayerObjectsCollector>();
            resolver.Resolve<PlayerDebugTool>();
            resolver.Resolve<PlayerStatsConfig>();
        }
    }
}