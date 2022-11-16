#region

using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Config_RangeAttack",
        menuName = PlayerAssetsPaths.RangeAttack + "Config")]
    public class RangeAttackConfigAsset : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _delay;
        [SerializeField] [Min(0f)] private float _startAimAngle;
        [SerializeField] [Min(0f)] private float _endAimAngle;
        [SerializeField] [Min(0f)] private float _aimTime;
        [SerializeField] [Min(0f)] private float _aimOverTime;
        [SerializeField] [Min(0f)] private float _aimAdditionalSpread;
        [SerializeField] [Min(0f)] private float _impactDistance;
        [SerializeField] [Min(0f)] private float _impactTime;

        public float Delay => _delay;
        public float StartAimAngle => _startAimAngle;
        public float EndAimAngle => _endAimAngle;
        public float AimTime => _aimTime;
        public float AimOverTime => _aimOverTime;
        public float AimAdditionalSpread => _aimAdditionalSpread;
        public float ImpactDistance => _impactDistance;
        public float ImpactTime => _impactTime;
    }
}