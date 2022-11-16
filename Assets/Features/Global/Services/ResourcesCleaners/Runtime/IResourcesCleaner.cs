using Cysharp.Threading.Tasks;

namespace Global.Services.ResourcesCleaners.Runtime
{
    public interface IResourcesCleaner
    {
        UniTask CleanUp();
    }
}