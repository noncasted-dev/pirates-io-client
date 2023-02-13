using GamePlay.Services.Network.RemoteEntities.Entity;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.Boardings.Local
{
    public interface IBoardingTargetSearcher
    {
        bool Search(Vector2 selfPosition, out IRemotePlayer target);
    }
}