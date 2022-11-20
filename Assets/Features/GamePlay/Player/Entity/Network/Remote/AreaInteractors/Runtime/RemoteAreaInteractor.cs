using GamePlay.Common.Areas.Common.Runtime;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Player.Entity.Views.Sprites.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.AreaInteractors.Runtime
{
    [DisallowMultipleComponent]
    public class RemoteAreaInteractor : MonoBehaviour, IAreaInteractor
    {
        [SerializeField] private Collider2D _collision;
        [SerializeField] private Collider2D _hitbox;
        [SerializeField] private PlayerSpriteView _spriteView;

        public bool IsLocal => false;
        public IShipResources Resources => new EmptyResources();

        public void OnCityEntered()
        {
            _hitbox.enabled = false;
            _collision.isTrigger = true;
        }

        public void OnCityExited()
        {
            _hitbox.enabled = true;
            _collision.isTrigger = false;
        }

        public void OnPortEntered()
        {
            _spriteView.Disable();
        }

        public void OnPortExited()
        {
            _spriteView.Enable();
        }
    }
}