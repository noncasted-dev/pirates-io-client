#region

using System;
using Common.ReadOnlyDictionaries.Runtime;
using GamePlay.Items.Abstract;

#endregion

namespace Global.Services.ItemFactories.Runtime
{
    [Serializable]
    public class ItemsConfigDictionary : ReadOnlyDictionary<ItemType, ItemAsset>
    {
    }
}