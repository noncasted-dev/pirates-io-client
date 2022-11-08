using UnityEngine;

namespace GamePlay.Player.Entity.Views.Arms.Runtime
{
    public class PlayerArmsView : MonoBehaviour, IArmsView
    {
        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;

        [SerializeField] private SpriteRenderer _leftSprite;
        [SerializeField] private SpriteRenderer _rightSprite;

        public void Rotate(float angle)
        {
            var rotation = Quaternion.Euler(0f, 0f, angle);

            _left.rotation = rotation;
            _right.rotation = rotation;
        }

        public void Snap(Vector2 position)
        {
            _left.position = position;
            _right.position = position;
        }

        public void SetFlipY(bool isFlipped)
        {
            _leftSprite.flipY = isFlipped;
            _rightSprite.flipY = isFlipped;
        }
    }
}