#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace Common.ReadOnlyDictionaries.Runtime
{
    public abstract class ReadOnlyDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private TKey[] _keys;
        [SerializeField] private TValue[] _values;

        public void OnAfterDeserialize()
        {
            if (_keys == null || _values == null)
                return;

            if (_keys.Length != _values.Length)
                return;

            Clear();

            var length = _keys.Length;

            for (var i = 0; i < length; ++i)
                this[_keys[i]] = _values[i];

            _keys = null;
            _values = null;
        }

        public virtual void OnBeforeSerialize()
        {
            var values = Enum.GetValues(typeof(TKey)).Cast<TKey>();
            var set = values.ToHashSet();

            foreach (var value in values)
            {
                if (ContainsKey(value) == true)
                    continue;

                this[value] = default;
            }

            var count = Count;
            _keys = new TKey[count];
            _values = new TValue[count];

            var i = 0;

            foreach (var (key, value) in this)
            {
                if (set.Contains(key) == false)
                    continue;

                _keys[i] = key;
                _values[i] = value;
                ++i;
            }
        }
    }
}