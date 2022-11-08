using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow;
using GamePlay.Player.Entity.Weapons.Bow.Components.Shooter;
using GamePlay.Player.Entity.Weapons.Bow.Views.Sprites.Runtime;
using GamePlay.Player.Entity.Weapons.Bow.Views.Transforms;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Weapons.Bow.Root
{
    public class BowRoot : MonoBehaviour, IBow
    {
        [Inject]
        private void Construct(
            IBowTransform bowTransform,
            IBowSprite sprite,
            IShooter shooter)
        {
            _shooter = shooter;
            _sprite = sprite;
            _bowTransform = bowTransform;
        }

        private IBowTransform _bowTransform;

        private IShooter _shooter;
        private IBowSprite _sprite;

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
            _bowTransform.SetPosition(position);
        }

        public void Rotate(float angle)
        {
            _bowTransform.SetRotation(angle);
        }

        public void SetFlipY(bool isFlipped)
        {
            _sprite.FlipY(isFlipped);
        }

        public void Shoot(float angle)
        {
            _shooter.Shoot(angle);
        }
    }
}