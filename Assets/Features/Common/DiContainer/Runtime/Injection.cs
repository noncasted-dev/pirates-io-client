using Common.DiContainer.Abstract;
using UnityEngine;
using VContainer;

namespace Common.DiContainer.Runtime
{
    public class Injection : IInjectionBuilder
    {
        public Injection(MonoBehaviour target)
        {
            _target = target;
        }

        private readonly MonoBehaviour _target;
        
        public void Inject(IObjectResolver resolver)
        {
            resolver.Inject(_target);
        }
    }
}