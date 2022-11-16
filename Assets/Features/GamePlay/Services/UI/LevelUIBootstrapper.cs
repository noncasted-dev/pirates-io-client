#region

using UnityEngine;
using VContainer;

#endregion

namespace GamePlay.Services.UI
{
    public class LevelUIBootstrapper : MonoBehaviour, ILevelUIBootstrapper
    {
        public Component[] ProcessInjection(IContainerBuilder builder)
        {
            return new Component[0];
        }
    }
}