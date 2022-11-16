#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Network.Views.Transforms.Runtime
{
    public interface INetworkTransform
    {
        void SetPosition(Vector2 position);
    }
}