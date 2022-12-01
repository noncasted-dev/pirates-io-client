using System;
using Common.Structs;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Sprites.Runtime
{
    [DisallowMultipleComponent]
    public class RemoteSpriteView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private Transform[] _scale;
        
        private Vector2 _previousPosition;

        private void FixedUpdate()
        {
            var position = (Vector2)transform.position;

            if (_previousPosition == position)
                return;

            var delta = position.x - _previousPosition.x;

            var flip = DirectionUtils.ToHorizontal(delta);

            var isFLipped = flip switch
            {
                Horizontal.Right => false,
                Horizontal.Left => true,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (isFLipped == true)
            {
                foreach (var scalable in _scale)
                    scalable.localScale = new Vector3(-1, 1, 1f);
            }
            else
            {
                foreach (var scalable in _scale)
                    scalable.localScale = new Vector3(1, 1, 1f);
            }
            foreach (var spriteRenderer in _spriteRenderers)
                spriteRenderer.flipX = isFLipped;

            _previousPosition = position;
        }
    }
}