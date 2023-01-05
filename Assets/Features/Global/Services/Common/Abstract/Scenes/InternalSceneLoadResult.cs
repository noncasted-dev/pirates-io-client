using UnityEngine.ResourceManagement.ResourceProviders;

namespace Global.Services.Common.Abstract.Scenes
{
    public class InternalSceneLoadResult<T>
    {
        public InternalSceneLoadResult(SceneInstance instance, T searched)
        {
            Instance = instance;
            Searched = searched;
        }

        public readonly SceneInstance Instance;
        public readonly T Searched;
    }
}