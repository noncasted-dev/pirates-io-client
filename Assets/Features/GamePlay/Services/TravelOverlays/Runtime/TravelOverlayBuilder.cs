using Common.DiContainer.Abstract;
using Common.Local.Services.Abstract.Callbacks;
using GamePlay.Services.TravelOverlays.Runtime.Health;
using UnityEngine;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    public class TravelOverlayBuilder : MonoBehaviour, ILocalContainerBuildListener
    {
        [SerializeField] private HealthBar _health;
        [SerializeField] private OverlayShipView _shipView;
        [SerializeField] private AimBar _aimBar;
        [SerializeField] private OverlayMenu _menu;
        [SerializeField] private ProjectileItemView[] _projectiles;

        public void OnContainerBuild(IDependencyRegister builder)
        {
            builder.Inject(_health);
            builder.Inject(_shipView);
            builder.Inject(_aimBar);
            builder.Inject(_menu);

            foreach (var projectile in _projectiles)
                builder.Inject(projectile);
        }
    }
}