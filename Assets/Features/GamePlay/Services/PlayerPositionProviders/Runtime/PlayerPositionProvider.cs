using UnityEngine;

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    public class PlayerPositionProvider : MonoBehaviour, IPlayerTransformPresenter, IPlayerPositionProvider
    {
        private Transform _player;

        public Vector2 Position => GetPosition();

        public void AssignPlayer(Transform player)
        {
            _player = player;
        }

        private Vector2 GetPosition()
        {
            if (_player == null)
            {
                Debug.Log("No player assigned");
                return Vector2.zero;
            }

            return _player.position;
        }
    }
}