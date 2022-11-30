using UnityEngine;

namespace GamePlay.Player.Entity.Views.Transforms.Runtime
{
    public interface IBodyTransform : ITransform
    {
        GameObject GameObject { get; }
    }
}