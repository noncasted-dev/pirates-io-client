using UnityEngine;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public interface IShipResourcesPresenter
    {
        void SetName(string name);
        void SetIcon(Sprite icon);
        
        void SetMaxWeight(int maxWeight);
        void SetWeight(int weight);

        void SetMaxCannons(int maxCannons);
        void SetCannons(int cannons);

        void SetMaxTeam(int maxTeam);
        void SetTeam(int team);

        void SetMaxSpeed(int maxSpeed);
        void SetSpeed(int speed);
    }
}