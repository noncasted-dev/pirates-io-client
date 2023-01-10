using GamePlay.Cities.Instance.Root.Runtime;
using UnityEngine;

namespace Features.GamePlay.Services.PlayerPaths.Builder.Runtime
{
    public readonly struct PlayerPathBuildEvent
    {
        public PlayerPathBuildEvent(CityType target, Vector2[] path)
        {
            Target = target;
            Path = path;
        }

        public readonly CityType Target;
        public readonly Vector2[] Path;
    }
}