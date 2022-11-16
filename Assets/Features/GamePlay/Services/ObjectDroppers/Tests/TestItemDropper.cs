#region

using GamePlay.Items.Abstract;
using GamePlay.Services.ObjectDroppers.Presenter.Runtime;
using NaughtyAttributes;
using UnityEngine;

#endregion

namespace GamePlay.Services.ObjectDroppers.Tests
{
    public class TestItemDropper : MonoBehaviour
    {
        [SerializeField] private ItemAsset _item;

        [Button("Drop")]
        private void Drop()
        {
            var dropper = FindObjectOfType<ObjectDropper>();

            var item = _item.Create(12);

            dropper.Drop(item, transform.position);
        }
    }
}