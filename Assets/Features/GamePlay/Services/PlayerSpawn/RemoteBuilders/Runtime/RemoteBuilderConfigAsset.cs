using GamePlay.Common.Paths;
using GamePlay.Player.Entity.Components.Definition;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "RemoteBuilder",
        menuName = GamePlayAssetsPaths.RemotePlayerBuilder + "Config")]
    public class RemoteBuilderConfigAsset : ScriptableObject
    {
        [SerializeField] private ShipsDictionary _ships;

        public AssetReference GetShip(ShipType type)
        {
            return _ships[type].Remote;
        }

        public AssetReference GetDead(ShipType type)
        {
            return _ships[type].Dead;
        }
    }
}