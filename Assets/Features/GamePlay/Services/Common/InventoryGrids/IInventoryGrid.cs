using System;
using GamePlay.Items.Abstract;

namespace GamePlay.Services.Common.InventoryGrids
{
    public interface IInventoryGrid
    {
        event Action<IItem, GridCell> Clicked;
        void Fill(IItem[] items);
    }
}