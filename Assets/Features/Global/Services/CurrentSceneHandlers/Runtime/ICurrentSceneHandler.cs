using Cysharp.Threading.Tasks;
using Local.ComposedSceneConfig;

namespace Global.Services.CurrentSceneHandlers.Runtime
{
    public interface ICurrentSceneHandler
    {
        public void OnLoaded(ComposedSceneLoadResult loaded);

        public UniTask Unload();

        public UniTask FinalizeUnloading();
    }
}