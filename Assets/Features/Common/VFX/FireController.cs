using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _baseParticleSystem;
    [SerializeField] private ParticleSystem _fireEmmiterParticleSystem;
    [SerializeField] private Vector2 _EmmitRange;
    [SerializeField] [Range(0f,1f)]private float _value;
    private float _valueLast;

    /// <summary>
    /// Принимает значение от 0 до 1, где 0 это 0 HP а 1 это aekk HP
    /// </summary>
    public void SetFireForce(float value)
    {
        value = Mathf.Clamp01(value);
        _value = value;
        if (_value > 0.5f)
            _baseParticleSystem.Stop();
        else
        {
            _baseParticleSystem.Play();
            var module = _fireEmmiterParticleSystem.emission;
            float t = Mathf.Clamp01(((_value  * 2f) - 0.2f) / 0.8f);
            float circleArea = 2f * Mathf.PI * _fireEmmiterParticleSystem.shape.radius;
            module.rateOverTime = Mathf.Lerp(_EmmitRange.x, _EmmitRange.y, t) * circleArea;
        }
    }

    private void OnValidate()
    {
        if (_value != _valueLast)
        {
            _valueLast = _value;
            SetFireForce(_value);
        }
    }
}
