using System;
using Common.DiContainer.Abstract;
using UnityEngine;
using VContainer;

namespace Common.DiContainer.Runtime
{
    public class Registration : IRegistration, IRegistrationBuilder
    {
        public Registration(RegistrationBuilder registration, Type type)
        {
            _registration = registration;
            _type = type;
        }

        private readonly RegistrationBuilder _registration;
        private readonly Type _type;

        private bool _isListeningCallbacks;
        private bool _isSelfResolvable;

        public IRegistration AsCallbackListener()
        {
            _isListeningCallbacks = true;

            return AsSelfResolvable();
        }

        public IRegistration AsSelf()
        {
            _registration.AsSelf();

            return this;
        }

        public IRegistration AsImplementedInterfaces()
        {
            _registration.AsImplementedInterfaces();

            return this;
        }

        public IRegistration AsSelfResolvable()
        {
            _isSelfResolvable = true;
            _registration.AsSelf();

            return this;
        }

        public IRegistration As<T>()
        {
            _registration.As<T>();

            return this;
        }

        public IRegistration WithParameter<T>(T value)
        {
            _registration.WithParameter(value);

            return this;
        }

        public void Register(IContainerBuilder builder)
        {
            builder.Register(_registration);
        }

        public void Resolve(IObjectResolver resolver)
        {
            if (_isSelfResolvable == false)
                return;

            resolver.Resolve(_type);
        }

        public void ResolveWithCallbacks(IObjectResolver resolver, ICallbackRegister callbackRegistry)
        {
            if (_isSelfResolvable == false)
                return;

            var registration = resolver.Resolve(_type);

            if (_isListeningCallbacks == true)
                callbackRegistry.Listen(registration);
        }
    }
}