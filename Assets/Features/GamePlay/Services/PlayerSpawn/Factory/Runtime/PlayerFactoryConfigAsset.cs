﻿using System.Collections.Generic;
using GamePlay.Common.Paths;
using GamePlay.Player.Entity.Components.Definition;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "PlayerFactory",
        menuName = GamePlayAssetsPaths.PlayerFactory + "Config")]
    public class PlayerFactoryConfigAsset : ScriptableObject
    {
        [SerializeField] private GameObject _networkPrefab;
        [SerializeField] private GameObject _botPrefab;

        [SerializeField] private ShipsDictionary _ships;
        [SerializeField] private List<AssetReference> _botShips;

        public GameObject NetworkPrefab => _networkPrefab;
        public GameObject BotPrefab => _botPrefab;

        public AssetReference GetShip(ShipType type)
        {
            return _ships[type].Local;
        }

        public AssetReference GetBotShip(ShipType type)
        {
            return _botShips[(int)type];
        }
    }
}