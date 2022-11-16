#region

using Cysharp.Threading.Tasks;

#endregion

namespace Global.Services.ResourcesCleaners.Runtime
{
    public interface IResourcesCleaner
    {
        UniTask CleanUp();
    }
}