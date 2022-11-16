#region

using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.Sprites.Runtime
{
    public interface ISpriteMaterial
    {
        Material Material { get; }

        void SetMaterial(Material material);
    }
}