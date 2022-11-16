#region

using System;
using Common.ReadOnlyDictionaries.Runtime;
using UnityEngine;

#endregion

namespace GamePlay.Player.Entity.Views.Pivots.Runtime
{
    [Serializable]
    public class PivotsDictionary : ReadOnlyDictionary<PivotType, Transform>
    {
    }
}