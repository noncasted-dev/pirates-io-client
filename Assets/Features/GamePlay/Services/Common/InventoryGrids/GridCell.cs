#region

using System;
using GamePlay.Items.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace GamePlay.Services.Common.InventoryGrids
{
    [DisallowMultipleComponent]
    public class GridCell : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        private int _id;
        private Item _item;

        public int Id => _id;

        public event Action<GridCell> Selected;

        public Item Item => _item;

        public void Setup(int id)
        {
            _id = id;
        }

        public void AssignItem(Item item)
        {
            gameObject.SetActive(true);

            _icon.sprite = item.BaseData.Icon;
            _count.text = item.Count.ToString();

            _item = item;
        }

        public void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Selected?.Invoke(this);
        }
    }
}