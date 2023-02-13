using GamePlay.Cities.Instance.Root.Runtime;

namespace GamePlay.Services.PlayerPaths.Builder.Runtime
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