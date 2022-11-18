using System.Collections.Generic;
using Features.Global.Services.InputViews.ConstraintsStorage;
using Global.Common;
using Global.Services.InputViews.Constraints;
using UnityEngine;

namespace Global.Services.UiStateMachines.Runtime
{
    [CreateAssetMenu(fileName =  "UiConstraints_", menuName = GlobalAssetsPaths.UiStateMachine + "Constraints")]
    public class UiConstraints : ScriptableObject
    {
        [SerializeField] private InputConstraintsDictionary _input;

        public IReadOnlyDictionary<InputConstraints, bool> Input => _input;
    }
}