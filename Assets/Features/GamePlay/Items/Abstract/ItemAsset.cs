using UnityEngine;

namespace GamePlay.Items.Abstract
{
    public abstract class ItemAsset : ScriptableObject
    {
        [SerializeField] private int _weight;
        [SerializeField] private string _name;
        [SerializeField] private bool _isInfinite;
        [SerializeField] private Sprite _icon;

        protected abstract ItemType Type { get; }

        public IItem Create(int count)
        {
            var data = new BaseItemData(_name, _weight, Type, _icon, _isInfinite);

            return BuildItem(data, count);
        }

        protected abstract IItem BuildItem(BaseItemData data, int count);
    }
}