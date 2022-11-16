using GamePlay.Common.Areas.Common.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.AreaInteractors.Runtime
{
    [DisallowMultipleComponent]
    public class RemoteAreaInteractor : MonoBehaviour, IAreaInteractor
    {
        [SerializeField] private Collider2D _collision;
        [SerializeField] private Collider2D _hitbox;

        public void OnCityEntered()
        {
            _hitbox.enabled = false;
            _collision.isTrigger = true;
        }

        public void OnAreaExited()
        {
            _hitbox.enabled = true;
            _collision.isTrigger = false;
        }
    }
}