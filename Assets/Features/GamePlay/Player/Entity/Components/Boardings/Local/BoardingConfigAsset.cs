using GamePlay.Player.Entity.Setup.Path;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Boardings.Local
{
    [InlineEditor]
    [CreateAssetMenu(fileName = PlayerAssetsPaths.ConfigPrefix + "Boarding", menuName = PlayerAssetsPaths.Boarding + "Config")]
    public class BoardingConfigAsset : ScriptableObject
    {
        [SerializeField] private float _triggerDistance;
        [SerializeField] private float _triggerTime;
        [SerializeField] private float _tickTime;

        public float TriggerDistance => _triggerDistance;
        public float TriggerTime => _triggerTime;
        public float TickTime => _tickTime;
    }
}