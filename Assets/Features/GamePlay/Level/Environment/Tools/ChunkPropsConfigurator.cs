using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;

namespace GamePlay.Level.Environment.Tools
{
    public class ChunkPropsConfigurator : MonoBehaviour
    {
        [Button("Configure")]
        private void Configure()
        {
            var childs = new GameObject[transform.childCount];

            for (var i = 0; i < childs.Length; i++)
                childs[i] = transform.GetChild(i).gameObject;


            foreach (var child in childs)
            {
                if (child.TryGetComponent(out SortingGroup sortingGroup) == true)
                    continue;

                var group = child.AddComponent<SortingGroup>();
                group.sortingLayerName = "Location";
                group.sortingOrder = 20;
            }
        }
    }
}