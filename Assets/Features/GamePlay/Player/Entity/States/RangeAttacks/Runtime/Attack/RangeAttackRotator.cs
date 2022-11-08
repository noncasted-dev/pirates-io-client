using Common.Structs;
using GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract;
using GamePlay.Player.Entity.Views.Arms.Runtime;
using GamePlay.Player.Entity.Views.Pivots.Runtime;
using GamePlay.Player.Entity.Weapons.Handler.Runtime;
using UnityEngine;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Attack
{
    public class RangeAttackRotator : IRangeAttackRotator
    {
        public RangeAttackRotator(
            IRotation rotation,
            ISpriteRotation spriteRotation,
            IAnimatorRotation animatorRotation,
            IWeaponsHandler weapons,
            IArmsView arms,
            IPivots pivots)
        {
            _rotation = rotation;
            _spriteRotation = spriteRotation;
            _animatorRotation = animatorRotation;
            _weapons = weapons;
            _arms = arms;
            _pivots = pivots;
        }

        private readonly IAnimatorRotation _animatorRotation;
        private readonly IArmsView _arms;
        private readonly IPivots _pivots;

        private readonly IRotation _rotation;
        private readonly ISpriteRotation _spriteRotation;
        private readonly IWeaponsHandler _weapons;

        public void Rotate(Vector2 direction)
        {
            var angle = _rotation.Angle;

            if (IsLookVertical(direction) == true)
            {
                if (angle is >= 22.5f and <= 157.5f)
                    direction.y = 1f;
                else
                    direction.y = -1f;

                angle = direction.GetAngle();
            }

            var pivot = angle switch
            {
                >= 0f and < 22.5f => PivotType.RangeAttack_Right_Face,
                >= 22.5f and <= 67.5f => PivotType.RangeAttack_Right_Back,
                > 67.5f and < 112.5f => PivotType.RangeAttack_Up,
                >= 112.5f and <= 157.5f => PivotType.RangeAttack_Left_Back,
                > 157.5f and < 247.5f => PivotType.RangeAttack_Left_Face,
                >= 247.5f and <= 292.5f => PivotType.RangeAttack_Down,
                > 292.5f and <= 360f => PivotType.RangeAttack_Right_Face,
                _ => PivotType.Default
            };

            var pivotPosition = _pivots.GetPosition(pivot);

            if (pivot is PivotType.RangeAttack_Left_Back or PivotType.RangeAttack_Left_Face)
            {
                _arms.SetFlipY(true);
                _weapons.Bow.SetFlipY(true);
            }

            _arms.Snap(pivotPosition);
            _weapons.Bow.Snap(pivotPosition);

            _animatorRotation.Rotate(angle);
            _spriteRotation.ResetRotation();
            _spriteRotation.RotateX(false);

            _weapons.Bow.Rotate(_rotation.Angle);
            _arms.Rotate(_rotation.Angle);
        }

        public void ToDefault()
        {
            _weapons.Bow.Rotate(0f);
            _arms.Rotate(0f);

            var pivotPosition = _pivots.GetPosition(PivotType.Default);

            _arms.Snap(pivotPosition);
            _weapons.Bow.Snap(pivotPosition);
            _arms.SetFlipY(false);
            _weapons.Bow.SetFlipY(false);
        }

        private bool IsLookVertical(Vector2 direction)
        {
            if (_rotation.Angle is > 67.5f and < 112.5f or > 247.5f and < 292.5f == false)
                return false;

            if (direction.IsVertical() == true || direction.IsZero() == true)
                return false;

            return true;
        }
    }
}