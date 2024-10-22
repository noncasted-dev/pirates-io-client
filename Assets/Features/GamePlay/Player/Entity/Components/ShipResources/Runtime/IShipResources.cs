﻿using System;
using UnityEngine;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public interface IShipResources
    {
        string Name { get; }
        Sprite Icon { get; }
        int MaxHealth { get; }
        int Health { get; }

        int MaxWeight { get; }
        int Weight { get; }

        int MaxCannons { get; }
        int Cannons { get; }

        float MaxSpeed { get; }
        int Speed { get; }

        int MaxTeam { get; }
        int Team { get; }

        int Sail { get; }

        bool IsShallowIgnored { get; }
        int ShallowDamage { get; }

        event Action<int, int> HealthChanged;
        event Action<int, int> WeightChanged;
        event Action<int, int> CannonsChanged;
        event Action<int, int> SpeedChanged;
        event Action<int, int> TeamChanged;
    }
}