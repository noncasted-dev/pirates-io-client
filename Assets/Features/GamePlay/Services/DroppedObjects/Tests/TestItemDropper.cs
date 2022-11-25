using GamePlay.Items.Abstract;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Tests
{
    public class TestItemDropper : MonoBehaviour
    {
        [SerializeField] private ItemAsset _item;

        [Button("Drop")]
        private void Drop()
        {
            var dropper = FindObjectOfType<DroppedObjectsPresenter>();

            var item = _item.Create(12);

            dropper.DropFromDeath(item.BaseData.Type, item.Count, transform.position);
        }
    }
}