﻿using GamePlay.Common.Paths;
using UnityEngine;

namespace GamePlay.Services.Projectiles.Mover
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "ProjectilesMover",
        menuName = GamePlayAssetsPaths.Projectiles + "Config")]
    public class ProjectilesMoverConfigAsset : ScriptableObject
    {
        [SerializeField] private LayerMask _allInteractionsMask;
        [SerializeField] [Min(0)] private int _bufferSize;
        [SerializeField] private string _hitBoxLayer;

        public string HitBoxLayer => _hitBoxLayer;
        public LayerMask AllInteractionsMask => _allInteractionsMask;
        public int BufferSize => _bufferSize;
    }
}