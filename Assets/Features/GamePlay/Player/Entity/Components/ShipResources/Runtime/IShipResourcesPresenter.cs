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

        void SetMaxSpeed(float maxSpeed);
        void SetSpeed(int speed);

        void SetShallowIgnorance(bool isIgnored);
        void SetShallowDamage(int damage);
    }
}