using UnityEngine;
using VContainer;

namespace GamePlay.Services.UI
{
    public interface ILevelUIBootstrapper
    {
        Component[] ProcessInjection(IContainerBuilder builder);
    }
}