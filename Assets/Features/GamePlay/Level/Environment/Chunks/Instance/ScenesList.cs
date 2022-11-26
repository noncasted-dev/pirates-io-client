using System.Collections.Generic;
using GamePlay.Common.Paths;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Level.Environment.Chunks.Instance
{
    [CreateAssetMenu(fileName = "ScenesList", menuName = GamePlayAssetsPaths.Config + "ScenesList")]
    public class ScenesList : ScriptableObject
    {
        [SerializeField] private List<AssetReference> _scenes;

        public IReadOnlyList<AssetReference> Scenes => _scenes;

        public void Add(AssetReference scene)
        {
            _scenes.Add(scene);
        }
    }
}