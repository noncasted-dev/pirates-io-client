using GamePlay.Player.Entity.Components.ShipResources.Runtime;

namespace GamePlay.Common.Areas.Common.Runtime
{
    public interface IAreaInteractor
    {
        bool IsLocal { get; }
        IShipResources Resources { get; }

        void OnCityEntered();
        void OnCityExited();

        void OnPortEntered();
        void OnPortExited();
    }
}