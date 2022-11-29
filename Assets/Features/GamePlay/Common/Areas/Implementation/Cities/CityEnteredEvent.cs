using GamePlay.Cities.Instance.Root.Runtime;

namespace GamePlay.Common.Areas.Implementation.Cities
{
    public readonly struct CityEnteredEvent
    {
        public CityEnteredEvent(CityDefinition city)
        {
            City = city;
        }
        
        public readonly CityDefinition City;
    }
}