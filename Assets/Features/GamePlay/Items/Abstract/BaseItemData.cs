using UnityEngine;

namespace GamePlay.Items.Abstract
{
    public class BaseItemData
    {
        public BaseItemData(
            string name,
            int weight,
            ItemType type,
            Sprite icon,
            bool isInfinite)
        {
            Name = name;
            Weight = weight;
            Type = type;
            Icon = icon;
            IsInfinite = isInfinite;
        }

        public string Name { get; }
        public int Weight { get; }
        public ItemType Type { get; }
        public Sprite Icon { get; }
        public bool IsInfinite { get; }
    }
}