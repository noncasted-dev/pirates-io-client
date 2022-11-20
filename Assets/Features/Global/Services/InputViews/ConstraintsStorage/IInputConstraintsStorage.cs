using System.Collections.Generic;
using Global.Services.InputViews.Constraints;

namespace Global.Services.InputViews.ConstraintsStorage
{
    public interface IInputConstraintsStorage
    {
        bool this[InputConstraints key] { get; }

        void Add(IReadOnlyDictionary<InputConstraints, bool> constraint);
        void Remove(IReadOnlyDictionary<InputConstraints, bool> constraint);
    }
}