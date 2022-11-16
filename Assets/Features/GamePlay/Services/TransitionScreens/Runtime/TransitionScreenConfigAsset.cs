#region

using GamePlay.Common.Paths;
using UnityEngine;

#endregion

namespace GamePlay.Services.TransitionScreens.Runtime
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "TransitionScreen",
        menuName = GamePlayAssetsPaths.TransitionScreen + "Config")]
    public class TransitionScreenConfigAsset : ScriptableObject
    {
        [SerializeField] private float _fadeSpeed;

        public float FadeSpeed => _fadeSpeed;
    }
}