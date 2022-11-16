#region

using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorTriggerPrefix + "TriggerName",
        menuName = PlayerAssetsPaths.Animator + "Trigger")]
    public class AnimationTriggerAsset : ScriptableObject
    {
        [SerializeField] private string _triggerName;
        [SerializeField] private bool _mirrorToSubAnimators;

        [SerializeField] [Tooltip("To override time animation duration should equal 1 second")]
        private bool _overrideTime;

        [SerializeField] [Min(0f)] private float _time;

        public AnimationTrigger CreateTrigger()
        {
            return new AnimationTrigger(
                _triggerName,
                _mirrorToSubAnimators,
                _overrideTime,
                _time);
        }
    }
}