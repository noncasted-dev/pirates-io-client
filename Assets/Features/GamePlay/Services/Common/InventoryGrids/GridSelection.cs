using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Services.Common.InventoryGrids
{
    public class GridSelection : MonoBehaviour
    {
        private GridCell _selectedCell;
        private IItem _selectedItem;

        public bool IsSelected(GridCell cell)
        {
            if (_selectedCell == null)
                return false;

            if (_selectedItem == null)
                return false;

            if (_selectedCell != cell)
                return false;

            return true;
        }

        public void Move(GridCell cell)
        {
            gameObject.SetActive(true);

            _selectedCell = cell;
            _selectedItem = cell.Item;

            transform.position = cell.transform.position;
        }

        public void Disable()
        {
            _selectedCell = null;
            _selectedItem = null;

            gameObject.SetActive(false);
        }

        public void UpdateSelection()
        {
            if (_selectedCell == null)
                return;

            if (_selectedCell.Item == null)
            {
                gameObject.SetActive(false);
                return;
            }

            if (_selectedCell.Item == _selectedItem)
                return;

            _selectedCell = null;
            _selectedItem = null;

            gameObject.SetActive(false);
        }
    }
}