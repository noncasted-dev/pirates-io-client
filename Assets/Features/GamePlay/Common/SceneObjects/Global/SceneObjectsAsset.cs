using Common.EditableScriptableObjects.Attributes;
using GamePlay.Common.SceneObjects.Logs;
using Global.Common;
using Global.Services.Common.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Common.SceneObjects.Global
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ServicePrefix + "SceneObjects",
        menuName = GlobalAssetsPaths.SceneObjects + "Service", order = 1)]
    public class SceneObjectsAsset : GlobalServiceAsset
    {
        [SerializeField]  private SceneObjectLogSettings _logSettings;

        public override void Create(IContainerBuilder builder, IServiceBinder serviceBinder)
        {
            builder.Register<SceneObjectLogger>(Lifetime.Singleton)
                .WithParameter(_logSettings);
        }
    }
}