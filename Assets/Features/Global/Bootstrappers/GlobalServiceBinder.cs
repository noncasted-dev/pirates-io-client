using Global.Services.Common.Abstract;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Bootstrappers
{
    public class GlobalServiceBinder : IGlobalServiceBinder
    {
        public GlobalServiceBinder(Scene scene)
        {
            _modulesTransformer = new ModulesTransformer(scene);
        }

        private readonly ModulesTransformer _modulesTransformer;

        public void AddToModules(MonoBehaviour service)
        {
            _modulesTransformer.AddModule(service);
        }
    }
}