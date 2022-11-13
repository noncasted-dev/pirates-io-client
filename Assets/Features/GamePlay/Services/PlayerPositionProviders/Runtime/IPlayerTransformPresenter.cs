using UnityEngine;

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    public interface IPlayerTransformPresenter
    {
        void AssignPlayer(Transform player);
    }
}