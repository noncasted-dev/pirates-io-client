#region

using Global.Services.ScenesFlow.Handling.Result;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

#endregion

namespace Global.Services.ScenesFlow.Handling.Data
{
    public class EmptySceneLoadData : SceneLoadData<EmptySceneLoadResult>
    {
        public EmptySceneLoadData(AssetReference asset) : base(asset)
        {
        }

        public override EmptySceneLoadResult CreateLoadResult(SceneInstance scene)
        {
            return new EmptySceneLoadResult(scene);
        }
    }
}