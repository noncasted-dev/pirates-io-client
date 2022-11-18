using System;
using Common.ReadOnlyDictionaries.Runtime;
using Global.Services.InputViews.Constraints;

namespace Features.Global.Services.InputViews.ConstraintsStorage
{
    [Serializable]
    public class InputConstraintsDictionary : ReadOnlyDictionary<InputConstraints, bool>
    {   
        
    }
}