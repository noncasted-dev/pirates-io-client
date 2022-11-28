using System;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public class EmptyResources : IShipResources
    {
        public string Name => "NotInitialized";
        public Sprite Icon => null;
        public int MaxHealth => 0;
        public int Health => 0;
        public int MaxWeight => 0;
        public int Weight => 0;
        public int MaxCannons => 0;
        public int Cannons => 0;
        public int MaxSpeed => 0;
        public int Speed => 0;
        public int MaxTeam => 0;
        public int Team => 0;
        public int Sail { get; }
        public bool IsShallowIgnored { get; }
        public int ShallowDamage { get; }

        public event Action<int, int> HealthChanged;
        public event Action<int, int> WeightChanged;
        public event Action<int, int> CannonsChanged;
        public event Action<int, int> SpeedChanged;
        public event Action<int, int> TeamChanged;
    }
}