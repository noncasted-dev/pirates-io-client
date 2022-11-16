#region

using UnityEngine;

#endregion

namespace Common.ObjectsPools.Runtime.Abstract
{
    public interface IObjectProvider<out T>
    {
        T Get(Vector2 position);
    }
}