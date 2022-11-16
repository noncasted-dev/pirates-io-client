using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorFloatPrefix + "Float",
        menuName = PlayerAssetsPaths.Animator + "Float")]
    public class AnimatorFloatAsset : ScriptableObject
    {
        [SerializeField] private bool _mirrorToSubAnimators;
        [SerializeField] private string _triggerName;

        public AnimatorFloat CreateFloat()
        {
            return new AnimatorFloat(_triggerName, _mirrorToSubAnimators);
        }
    }
}