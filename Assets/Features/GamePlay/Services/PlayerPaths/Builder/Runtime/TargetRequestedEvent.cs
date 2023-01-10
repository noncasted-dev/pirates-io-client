using System;
using GamePlay.Cities.Instance.Root.Runtime;

namespace Features.GamePlay.Services.PlayerPaths.TargetSelection.Runtime
{
    public readonly struct TargetRequestedEvent
    {
        public TargetRequestedEvent(CityType target)
        {
            Target = target;
        }
        
        public readonly CityType Target;
    }
}