#region

using GamePlay.Player.Entity.Setup.Bootstrap;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Weapons.Cannon.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Weapons.Cannon.Views.Transforms;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace GamePlay.Player.Entity.Weapons.Cannon.Views.Bootstrap
{
    [DisallowMultipleComponent]
    public class CannonViewsBootstrapper : MonoBehaviour, IPlayerContainerBuilder
    {
        [SerializeField] private CannonSprite _sprite;
        [SerializeField] private CannonTransform _transform;

        public void OnBuild(IContainerBuilder builder)
        {
            builder.RegisterComponent(_sprite).As<ICannonSprite>();
            builder.RegisterComponent(_transform).As<ICannonTransform>();
        }

        public void Resolve(IObjectResolver resolver, ICallbackRegister callbackRegister)
        {
            callbackRegister.Add(_sprite);
            callbackRegister.Add(_transform);
        }
    }
}