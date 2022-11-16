#region

using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;

#endregion

namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IObjectsPoolHandler
    {
        void Setup(IObjectResolver resolver, Scene targetScene);
        UniTask Prepare();
        void InstantiateStartupInstances();
    }
}