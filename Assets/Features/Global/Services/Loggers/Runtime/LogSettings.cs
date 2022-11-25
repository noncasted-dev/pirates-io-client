using System;
using Common.ReadOnlyDictionaries.Runtime;
using UnityEngine;

namespace Global.Services.Loggers.Runtime
{
    public abstract class LogSettings<TDictionary, TEnum> : ScriptableObject
        where TDictionary : ReadOnlyDictionary<TEnum, bool>
        where TEnum : Enum
    {
        [SerializeField] private bool _isEnabled = true;
        [SerializeField] private TDictionary _logs;

        public bool IsAvailable(TEnum logType)
        {
            if (_isEnabled == false)
                return false;

            if (_logs.ContainsKey(logType) == false|| _logs[logType] == false)
                return false;

            if (ConfirmAvailability(logType) == false)
                return false;

            return true;
        }

        protected virtual bool ConfirmAvailability(TEnum logType)
        {
            return true;
        }
    }
}