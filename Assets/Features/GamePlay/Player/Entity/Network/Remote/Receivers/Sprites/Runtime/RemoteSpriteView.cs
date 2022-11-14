using System;
using Common.Structs;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Sprites.Runtime
{
    [DisallowMultipleComponent]
    public class RemoteSpriteView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _spriteRenderers;

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

            foreach (var spriteRenderer in _spriteRenderers)
                spriteRenderer.flipX = isFLipped;

            _previousPosition = position;
        }
    }
}