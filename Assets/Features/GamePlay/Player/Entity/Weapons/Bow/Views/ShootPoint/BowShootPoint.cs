using System;
using Common.Structs;
using GamePlay.Player.Entity.Weapons.Bow.Views.Transforms;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Weapons.Bow.Views.ShootPoint
{
    [DisallowMultipleComponent]
    public class BowShootPoint : MonoBehaviour, IShootPoint
    {
        [Inject]
        private void Construct(IBowTransform bowTransform)
        {
            _bowTransform = bowTransform;
        }

        [SerializeField] private Transform _right;
        [SerializeField] private Transform _left;

        private IBowTransform _bowTransform;

        public Vector2 GetShootPoint()
        {
            Vector2 shootPosition = _bowTransform.Look switch
            {
                Horizontal.Right => _right.position,
                Horizontal.Left => _left.position,
                _ => throw new ArgumentOutOfRangeException()
            };

            return shootPosition;
        }
    }
}