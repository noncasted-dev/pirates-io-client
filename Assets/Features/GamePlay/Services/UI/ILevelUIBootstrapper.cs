#region

using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Services.UI
{
    public interface ILevelUIBootstrapper
    {
        Component[] ProcessInjection(IContainerBuilder builder);
    }
}