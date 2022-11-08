using UnityEngine;

namespace GamePlay.Player.Entity.Views.Sprites.Runtime
{
    public interface ISpriteMaterial
    {
        Material Material { get; }

        void SetMaterial(Material material);
    }
}