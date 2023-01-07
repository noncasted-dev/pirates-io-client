using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.Definition;
using Global.Services.FilesFlow.Runtime.Abstract;

namespace GamePlay.Services.Saves.Definitions
{
    [Serializable]
    public class ShipSave : SaveData
    {
        public CityType LastCity;
        public ShipType ShipType;
        public List<ItemType> Items = new();
        public List<int> Count = new();
        public int Money;

        public ShipSave(string saveName) : base(saveName)
        {
        }

        public ShipSave() : base(nameof(SaveData))
        {
        }
    }
}