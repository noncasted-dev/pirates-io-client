﻿using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Player.Entity.Weapons.Cannon.Components.Shooter
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "BowShooter",
        menuName = PlayerAssetsPaths.BowShooter + "Config")]
    public class ShooterConfigAsset : ScriptableObject
    {
        [SerializeField] [Min(0f)] private int _damage;
        [SerializeField] [Min(0f)] private float _distance;

        [SerializeField] private AssetReference _ball;
        [SerializeField] private AssetReference _knuppel;
        [SerializeField] private AssetReference _shrapnel;
        [SerializeField] private AssetReference _fishnet;

        [SerializeField] [Min(0f)] private float _pushForce;
        [SerializeField] [Min(0f)] private float _randomDistance;
        [SerializeField] [Min(0f)] private float _shotRandomDelay;
        [SerializeField] [Min(0f)] private int _shotsAmount;
        [SerializeField] [Min(0f)] private float _shotsDelay;
        [SerializeField] [Min(0f)] private float _speed;

        [SerializeField] private AssetReference _vfx;

        public int ShotsAmount => _shotsAmount;
        public float ShotsDelay => _shotsDelay;
        public float ShotRandomDelay => _shotRandomDelay;
        public int Damage => _damage;
        public float Speed => _speed;
        public float Distance => _distance;
        public float RandomDistance => _randomDistance;
        public AssetReference Ball => _ball;
        public AssetReference Knuppel => _knuppel;
        public AssetReference Shrapnel => _shrapnel;
        public AssetReference Fishnet => _fishnet;
        public AssetReference Vfx => _vfx;
    }
}