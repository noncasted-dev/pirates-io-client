using Common.DiContainer.Abstract;
using Common.VFX;
using GamePlay.Player.Entity.Network.Local.AreaInteractors.Runtime;
using GamePlay.Player.Entity.Setup.Bootstrap;
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

        public void OnBuild(IDependencyRegister builder)
        {
            builder.RegisterComponent(_sprite)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            builder.RegisterComponent(_transform)
                .As<IBodyTransform>()
                .AsCallbackListener();

            builder.RegisterComponent(_rotationPoint)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            builder.RegisterComponent(_rigidBody)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            builder.RegisterComponent(_weaponsRoot)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            builder.RegisterComponent(_aim)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            builder.RegisterComponent(_shootPoint)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            builder.RegisterComponent(_spriteTransform)
                .As<ISpriteTransform>()
                .AsCallbackListener();

            builder.RegisterComponent(_areaInteractor)
                .AsCallbackListener();

            builder.RegisterComponent(_objectsCollector)
                .AsCallbackListener();

            builder.RegisterComponent(_debug)
                .AsCallbackListener();

            builder.RegisterComponent(_fireController)
                .AsCallbackListener();

            builder.RegisterComponent(_statsConfig)
                .AsCallbackListener();

            builder.RegisterComponent(_config)
                .As<IShipConfig>();
        }
    }
}