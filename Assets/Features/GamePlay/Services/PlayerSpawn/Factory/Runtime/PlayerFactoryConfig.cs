using GamePlay.Common.Paths;
using GamePlay.Player.Entity.Components.Definition;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "PlayerFactory",
        menuName = GamePlayAssetsPaths.PlayerFactory + "Config")]
    public class PlayerFactoryConfig : ScriptableObject
    {
        [SerializeField] private GameObject _networkPrefab;

        [SerializeField] private ShipsDictionary _ships;

        public GameObject NetworkPrefab => _networkPrefab;
        
        public AssetReference GetShip(ShipType type)
        {
            return _ships[type].Prefab;
        }
    }
}