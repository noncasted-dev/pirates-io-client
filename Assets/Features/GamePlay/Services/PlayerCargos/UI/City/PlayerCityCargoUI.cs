#region

using GamePlay.Items.Abstract;
using GamePlay.Services.Common.InventoryGrids;
using UnityEngine;

#endregion

namespace GamePlay.Services.PlayerCargos.UI.City
{
    public class PlayerCityCargoUI : MonoBehaviour, IPlayerCityCargoUI
    {
        [SerializeField] private GameObject _body;
        [SerializeField] private InventoryGrid _grid;

        public bool IsActive => _body.activeSelf;

        private void Awake()
        {
            _body.SetActive(false);
        }

        public void Open(IItem[] items)
        {
            _body.SetActive(true);
            _grid.Fill(items);
        }

        public void Close()
        {
            _body.SetActive(false);
        }
    }
}