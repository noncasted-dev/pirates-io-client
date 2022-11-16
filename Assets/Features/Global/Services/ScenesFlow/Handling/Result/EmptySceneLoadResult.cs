#region

using UnityEngine.ResourceManagement.ResourceProviders;

#endregion

namespace Global.Services.ScenesFlow.Handling.Result
{
    public class EmptySceneLoadResult : SceneLoadResult
    {
        public EmptySceneLoadResult(SceneInstance instance) : base(instance)
        {
        }
    }
}