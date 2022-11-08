using UnityEngine;
using VContainer;

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