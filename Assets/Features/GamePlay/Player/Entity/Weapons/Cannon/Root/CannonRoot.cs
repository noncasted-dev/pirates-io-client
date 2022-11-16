#region

using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow;
using GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter;
using GamePlay.Player.Entity.Weapons.Cannon.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Weapons.Cannon.Views.Transforms;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#endregion

namespace GamePlay.Player.Entity.Weapons.Cannon.Root
{
    public class CannonRoot : MonoBehaviour, ICanon
    {
        [Inject]
        private void Construct(
            ICannonTransform cannonTransform,
            ICannonSprite sprite,
            IShooter shooter)
        {
            _shooter = shooter;
            _sprite = sprite;
            _cannonTransform = cannonTransform;
        }

        private ICannonTransform _cannonTransform;

        private IShooter _shooter;
        private ICannonSprite _sprite;

        public string Name => gameObject.name;

        public async UniTask OnBootstrapped(IFlowHandler flowHandler, LifetimeScope parent)
        {
            flowHandler.InvokeAwake();
            await flowHandler.InvokeAsyncAwake();
            flowHandler.InvokeEnable();
            flowHandler.InvokeStart();
        }

        public void Snap(Vector2 position)
        {
            _cannonTransform.SetPosition(position);
        }

        public void Rotate(float angle)
        {
            _cannonTransform.SetRotation(angle);
        }

        public void SetFlipY(bool isFlipped)
        {
            _sprite.FlipY(isFlipped);
        }

        public void CancelShoot()
        {
            _shooter.Cancel();
        }

        public void Shoot(float angle, float spread)
        {
            _shooter.Shoot(angle, spread);
        }
    }
}