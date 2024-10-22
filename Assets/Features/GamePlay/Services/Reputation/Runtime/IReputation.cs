﻿using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Factions.Common;
using UnityEngine;

namespace GamePlay.Services.Reputation.Runtime
{
    public interface IReputation
    {
        int Value { get; }
        Sprite Flag { get; }
        FactionType Faction { get; }
        CityDefinition LastCity { get; }
    }
}