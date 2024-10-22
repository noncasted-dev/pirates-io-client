﻿using UnityEngine;

namespace GamePlay.Player.Entity.Components.Rotations.Runtime.Abstract
{
    public interface IRotation
    {
        float Angle { get; }
        Quaternion Quaternion { get; }
    }
}