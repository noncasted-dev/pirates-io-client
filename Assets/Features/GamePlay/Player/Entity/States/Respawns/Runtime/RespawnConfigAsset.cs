using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.States.Respawns.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Respawn",
        menuName = PlayerAssetsPaths.Respawn + "Config")]
    public class RespawnConfigAsset : ScriptableObject
    {
        [SerializeField] private int _maxHealth;

        public int MaxHealth => _maxHealth;
    }
}