using GamePlay.Player.Entity.Setup.Path;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Config_RangeAttack",
        menuName = PlayerAssetsPaths.RangeAttack + "Config")]
    public class RangeAttackConfigAsset : ScriptableObject
    {
        [SerializeField] [Min(0)] private int _damage;
        [SerializeField] [Min(0f)] private float _dashDistance;
        [SerializeField] [Min(0f)] private float _dashTime;
        [SerializeField] [Min(0f)] private float _delay;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)] [AllowNesting]
        private AnimationCurve _dashCurve;

        public int Damage => _damage;
        public float DashDistance => _dashDistance;
        public float DashTime => _dashTime;
        public float Delay => _delay;
        public AnimationCurve DashCurve => _dashCurve;
    }
}