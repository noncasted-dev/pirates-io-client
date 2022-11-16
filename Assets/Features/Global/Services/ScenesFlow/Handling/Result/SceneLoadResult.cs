#region

using UnityEngine.ResourceManagement.ResourceProviders;

#endregion

namespace Global.Services.ScenesFlow.Handling.Result
{
    public abstract class SceneLoadResult
    {
        public SceneLoadResult(SceneInstance instance)
        {
            Instance = instance;
        }

        public readonly SceneInstance Instance;
    }
}