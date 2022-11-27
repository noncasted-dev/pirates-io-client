using GamePlay.Services.TravelOverlays.Runtime.Health;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    public class TravelOverlayBuilder : MonoBehaviour
    {
        [SerializeField] private HealthBar _health;
        [SerializeField] private OverlayShipView _shipView;
        
        public void Register(IObjectResolver resolver)
        {
            resolver.Inject(_health);
            resolver.Inject(_shipView);
        }
    }
}