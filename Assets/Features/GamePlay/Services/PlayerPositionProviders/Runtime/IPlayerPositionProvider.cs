#region

using UnityEngine;

#endregion

namespace GamePlay.Services.PlayerPositionProviders.Runtime
{
    public interface IPlayerPositionProvider
    {
        Vector2 Position { get; }
    }
}