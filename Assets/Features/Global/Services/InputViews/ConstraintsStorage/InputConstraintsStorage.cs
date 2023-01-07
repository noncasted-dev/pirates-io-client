using System;
using System.Collections.Generic;
using System.Linq;
using Global.Services.InputViews.Constraints;
using Global.Services.InputViews.Logs;

namespace Global.Services.InputViews.ConstraintsStorage
{
    public class InputConstraintsStorage : IInputConstraintsStorage
    {
        public InputConstraintsStorage(InputViewLogger logger)
        {
            _logger = logger;
            var constraints = Enum.GetValues(typeof(InputConstraints)).Cast<InputConstraints>();

            foreach (var constraint in constraints)
                _constraints.Add(constraint, 0);
        }

        private readonly Dictionary<InputConstraints, int> _constraints = new();

        private readonly InputViewLogger _logger;

        public bool this[InputConstraints key] => _constraints[key] > 0;

        public void Add(IReadOnlyDictionary<InputConstraints, bool> constraints)
        {
            foreach (var (key, value) in constraints)
            {
                if (value == false)
                    continue;

                _constraints[key]++;

                _logger.OnConstraintAdded(key, _constraints[key]);
            }
        }

        public void Remove(IReadOnlyDictionary<InputConstraints, bool> constraints)
        {
            foreach (var (key, value) in constraints)
            {
                if (value == false)
                    continue;

                _constraints[key]--;

                var count = _constraints[key];

                _logger.OnConstraintReduced(key, count);

                if (count == 0)
                    _logger.OnConstraintRemoved(key);

                if (count < 0)
                {
                    _logger.OnConstraintBelowZeroException(key);
                    _constraints[key] = 0;
                }
            }
        }
    }
}