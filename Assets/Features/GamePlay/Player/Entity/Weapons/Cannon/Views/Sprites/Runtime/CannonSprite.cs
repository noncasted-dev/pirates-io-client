using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Weapons.Cannon.Views.Sprites.Runtime
{
    [DisallowMultipleComponent]
    public class CannonSprite : MonoBehaviour, IPlayerAwakeListener, IPlayerSwitchListener, ICannonSprite
    {
        [Inject]
        private void Construct(ISpriteView spriteView)
        {
            _spriteView = spriteView;
        }

        private SpriteRenderer _spriteRenderer;
        private ISpriteView _spriteView;

        public void OnAwake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FlipY(bool isFlipped)
        {
            _spriteRenderer.flipY = isFlipped;
        }

        public void OnEnabled()
        {
            _spriteView.AddSubSprite(_spriteRenderer);
        }

        public void OnDisabled()
        {
            _spriteView.RemoveSubSprite(_spriteRenderer);
        }
    }
}