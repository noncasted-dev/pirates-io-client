using System;
using Common.Structs;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Sprites.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class RemoteSpriteView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Vector2 _previousPosition;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            var position = (Vector2) transform.position;
            
            if (_previousPosition == position)
                return;

            var delta = position.x - _previousPosition.x;

            var flip = DirectionUtils.ToHorizontal(delta);

            _spriteRenderer.flipX = flip switch
            {
                Horizontal.Right => false,
                Horizontal.Left => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}