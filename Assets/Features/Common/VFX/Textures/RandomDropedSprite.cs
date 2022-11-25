using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
[ExecuteAlways]
public class RandomDropedSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> _flySprites;
    [SerializeField] private List<Sprite> _IdleSprites;
    [SerializeField] private List<GameObject> _randomSplashesVariants;

    public SpriteRenderer rA, rB;

    private void OnEnable()
    {
        int randomId = (int)(Random.value * _flySprites.Count);
        int randomSplashId = (int)(Random.value * _randomSplashesVariants.Count);
        bool randomBool = Random.value > 0.5f;
        rA.sprite = _flySprites[randomId];
        rB.sprite = _IdleSprites[randomId];
        rA.flipX = rB.flipX = randomBool;
        for (int i = 0; i < _randomSplashesVariants.Count; i++)
            _randomSplashesVariants[i].SetActive(i == randomSplashId);
    }
}
