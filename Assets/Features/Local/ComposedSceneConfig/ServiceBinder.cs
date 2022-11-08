using System.Collections.Generic;
using Local.Services.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Local.ComposedSceneConfig
{
    public class ServiceBinder : IServiceBinder
    {
        public ServiceBinder(LocalServiceTransformer transformer)
        {
            _serviceTransformer = transformer;
        }

        private readonly List<ComponentRegistration> _componentRegistrations = new();
        private readonly LocalServiceTransformer _serviceTransformer;
        private readonly List<RegistrationBuilder> _typeRegistrations = new();

        public void AddToModules(MonoBehaviour service)
        {
            _serviceTransformer.AddService(service);
        }

        public RegistrationBuilder Register<T>()
        {
            var builder = new RegistrationBuilder(typeof(T), Lifetime.Singleton);
            _typeRegistrations.Add(builder);

            return builder;
        }

        public ComponentRegistration RegisterComponent<T>(T component) where T : MonoBehaviour
        {
            var builder = new ComponentRegistration(component);
            _componentRegistrations.Add(builder);

            return builder;
        }

        public void RegisterAllQueued(IContainerBuilder builder)
        {
            foreach (var type in _typeRegistrations)
                builder.Register(type);

            foreach (var component in _componentRegistrations)
            {
                var componentBuilder = builder.RegisterComponent(component.Component);
                if (component.IsAsImplementedInterfaces == true)
                    componentBuilder.AsImplementedInterfaces();
                else
                    foreach (var type in component.Types)
                        componentBuilder.As(type);

                if (component.Types.Count != 0 && component.IsAsSelf == true)
                    component.AsSelf();

                foreach (var (key, value) in component.StringParameters)
                    componentBuilder.WithParameter(key, value);

                foreach (var (key, value) in component.TypeParameters)
                    componentBuilder.WithParameter(key, value);
            }
        }
    }
}