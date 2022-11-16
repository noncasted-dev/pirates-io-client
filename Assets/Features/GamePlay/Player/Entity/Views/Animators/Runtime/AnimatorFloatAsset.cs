#region

using GamePlay.Player.Entity.Setup.Path;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.Animators.Runtime
{
    [CreateAssetMenu(fileName = PlayerAssetsPaths.AnimatorFloatPrefix + "Float",
        menuName = PlayerAssetsPaths.Animator + "Float")]
    public class AnimatorFloatAsset : ScriptableObject
    {
        [SerializeField] private string _triggerName;
        [SerializeField] private bool _mirrorToSubAnimators;

        public AnimatorFloat CreateFloat()
        {
            return new AnimatorFloat(_triggerName, _mirrorToSubAnimators);
        }
    }
}