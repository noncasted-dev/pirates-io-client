using System.Collections.Generic;
using UnityEngine;

namespace Common.VFX.Textures
{
    [ExecuteAlways]
    public class RandomDropedSprite : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _flySprites;
        [SerializeField] private List<Sprite> _IdleSprites;
        [SerializeField] private List<GameObject> _randomSplashesVariants;

        public SpriteRenderer rA, rB;

        private void OnEnable()
        {
            var randomId = (int)(Random.value * _flySprites.Count);
            var randomSplashId = (int)(Random.value * _randomSplashesVariants.Count);
            var randomBool = Random.value > 0.5f;
            rA.sprite = _flySprites[randomId];
            rB.sprite = _IdleSprites[randomId];
            rA.flipX = rB.flipX = randomBool;
            for (var i = 0; i < _randomSplashesVariants.Count; i++)
                _randomSplashesVariants[i].SetActive(i == randomSplashId);
        }
    }
}