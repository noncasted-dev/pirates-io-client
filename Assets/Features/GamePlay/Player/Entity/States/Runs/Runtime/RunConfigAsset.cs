#region

using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Run", menuName = PlayerAssetsPaths.Run + "Config")]
    public class RunConfigAsset : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _speed;

        public float Speed => _speed;
    }
}