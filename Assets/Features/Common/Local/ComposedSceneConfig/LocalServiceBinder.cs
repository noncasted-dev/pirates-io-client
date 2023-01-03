using Common.Local.Services.Abstract;
using UnityEngine;

namespace Common.Local.ComposedSceneConfig
{
    public class LocalServiceBinder : ILocalServiceBinder
    {
        public LocalServiceBinder(LocalServiceTransformer transformer)
        {
            _serviceTransformer = transformer;
        }

        private readonly LocalServiceTransformer _serviceTransformer;

        public void AddToModules(MonoBehaviour service)
        {
            _serviceTransformer.AddService(service);
        }
    }
}