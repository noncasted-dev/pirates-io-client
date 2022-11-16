using System.Collections.Generic;
using GamePlay.Items.Abstract;
using Global.Common;
using UnityEngine;

namespace Global.Services.ItemFactories.Runtime
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "ItemFactory",
        menuName = GlobalAssetsPaths.ItemFactory + "Config")]
    public class ItemFactoryConfigAsset : ScriptableObject
    {
        [SerializeField] private ItemsConfigDictionary _items;

        public IReadOnlyDictionary<ItemType, ItemAsset> Items => _items;
    }
}