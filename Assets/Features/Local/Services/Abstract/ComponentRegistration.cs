using System;
using System.Collections.Generic;

namespace Local.Services.Abstract
{
    public class ComponentRegistration
    {
        public ComponentRegistration(object component)
        {
            Component = component;
        }

        private readonly Dictionary<string, object> _stringParameters = new();
        private readonly Dictionary<Type, object> _typeParameters = new();

        public readonly object Component;
        public readonly List<Type> Types = new();
        private bool _isAsImplementedInterfaces;

        private bool _isAsSelf;

        public bool IsAsSelf => _isAsSelf;

        public bool IsAsImplementedInterfaces => _isAsImplementedInterfaces;
        public IReadOnlyDictionary<string, object> StringParameters => _stringParameters;
        public IReadOnlyDictionary<Type, object> TypeParameters => _typeParameters;

        public ComponentRegistration As<T>()
        {
            Types.Add(typeof(T));

            return this;
        }

        public ComponentRegistration AsImplementedInterfaces()
        {
            _isAsImplementedInterfaces = true;

            return this;
        }

        public ComponentRegistration AsSelf()
        {
            _isAsSelf = true;

            return this;
        }

        public ComponentRegistration WithParameter(string name, object value)
        {
            _stringParameters.Add(name, value);

            return this;
        }

        public ComponentRegistration WithParameter<T>(object value)
        {
            _typeParameters.Add(typeof(T), value);

            return this;
        }
        
        public ComponentRegistration WithParameter<T>(T value)
        {
            _typeParameters.Add(typeof(T), value);

            return this;
        }
    }
}