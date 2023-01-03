using System.Collections.Generic;
using Common.DiContainer.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Common.DiContainer.Runtime
{
    public class ContainerBuilder : IDependencyRegister, IDependenciesBuilder
    {
        private readonly List<IRegistrationBuilder> _registrations = new();
        private readonly List<IInjectionBuilder> _injections = new();

        public void RegisterAll(IContainerBuilder builder)
        {
            foreach (var registration in _registrations)
                registration.Register(builder);
        }

        public void ResolveAll(IObjectResolver resolver)
        {
            foreach (var registration in _registrations)
                registration.Resolve(resolver);

            foreach (var injection in _injections)
                injection.Inject(resolver);
        }

        public void ResolveAllWithCallbacks(IObjectResolver resolver, ICallbackRegister callbackRegistry)
        {
            foreach (var registration in _registrations)
                registration.ResolveWithCallbacks(resolver, callbackRegistry);
            
            foreach (var injection in _injections)
                injection.Inject(resolver);
        }

        public IRegistration Register<T>()
        {
            var builder = new RegistrationBuilder(typeof(T), Lifetime.Singleton);
            var registration = new Registration(builder, typeof(T));
            _registrations.Add(registration);

            return registration;
        }

        public IRegistration RegisterComponent<T>(T component) where T : MonoBehaviour
        {
            var builder = new ComponentRegistrationBuilder(component);
            var registration = new Registration(builder, typeof(T));

            _registrations.Add(registration);

            return registration;
        }

        public void Inject<T>(T component) where T : MonoBehaviour
        {
            var injection = new Injection(component);
            
            _injections.Add(injection);
        }
    }
}