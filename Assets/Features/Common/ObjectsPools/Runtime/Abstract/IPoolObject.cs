using System;
using UnityEngine;

namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IPoolObject<T>
    {
        GameObject GameObject { get; }

        void SetPosition(Vector2 position);
        void SetupPoolObject(Action<T> returnToPool);
        void OnTaken();
        void OnReturned();
    }
}