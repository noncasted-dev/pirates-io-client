using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.States.Respawns.Runtime;
using GamePlay.Player.Entity.Views.Animators.Runtime;
using GamePlay.Player.Entity.Views.Arms.Runtime;
using GamePlay.Player.Entity.Views.Pivots.Runtime;
using GamePlay.Player.Entity.Views.RigidBodies.Runtime;
using GamePlay.Player.Entity.Views.RotationPoint;
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
        [SerializeField] private PlayerSpriteViewView _sprite;
        [SerializeField] private PlayerBodyTransform _transform;
        [SerializeField] private PlayerRotationPoint _rotationPoint;
        [SerializeField] private PlayerRigidBody _rigidBody;
        [SerializeField] private WeaponsRoot _weaponsRoot;

        public void OnBuild(IContainerBuilder builder)
        {
            builder.RegisterComponent(_sprite).AsImplementedInterfaces();
            builder.RegisterComponent(_transform).As<IBodyTransform>();
            builder.RegisterComponent(_rotationPoint).AsImplementedInterfaces();
            builder.RegisterComponent(_rigidBody).AsImplementedInterfaces();
            builder.RegisterComponent(_weaponsRoot).AsImplementedInterfaces();
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(_sprite);
            callbackRegister.Add(_transform);
            callbackRegister.Add(_rotationPoint);
            callbackRegister.Add(_rigidBody);
            callbackRegister.Add(_weaponsRoot);
        }
    }
}