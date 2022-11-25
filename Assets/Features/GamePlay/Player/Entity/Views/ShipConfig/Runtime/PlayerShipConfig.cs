using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Views.ShipConfig.Runtime
{
    [DisallowMultipleComponent]
    public class PlayerShipConfig : MonoBehaviour, IShipConfig
    {
        [SerializeField] private AssetReference _deathVfx;

        public AssetReference DeathVfx => _deathVfx;
    }
}