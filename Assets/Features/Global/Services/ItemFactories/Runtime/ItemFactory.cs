using GamePlay.Items.Abstract;
using UnityEngine;
using VContainer;

namespace Global.Services.ItemFactories.Runtime
{
    public class ItemFactory : MonoBehaviour, IItemFactory
    {
        [Inject]
        private void Construct(ItemFactoryConfigAsset configAsset)
        {
            _configAsset = configAsset;
        }

        private ItemFactoryConfigAsset _configAsset;

        public IItem Create(ItemType type, int count)
        {
            return _configAsset.Items[type].Create(count);
        }
    }
}