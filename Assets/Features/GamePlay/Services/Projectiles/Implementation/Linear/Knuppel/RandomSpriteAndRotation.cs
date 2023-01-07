using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Services.Projectiles.Implementation.Linear.Knuppel
{
    public class RandomSpriteAndRotation : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private SpriteRenderer renderer;

        private void OnEnable()
        {
            renderer.transform.rotation = quaternion.Euler(0, 0, Random.value * 360f);
            renderer.sprite = sprites[(int)(Random.value * sprites.Count)];
        }
    }
}