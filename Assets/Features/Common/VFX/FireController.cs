using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _baseParticleSystem;
    [SerializeField] private ParticleSystem _fireEmmiterParticleSystem;
    [SerializeField] private Vector2 _EmmitRange;
    
    [SerializeField] [Range(0f,1f)] private float _value;
    
    private float _lastValue;
    private bool _isActive = true;

    private void Start()
    {
        _baseParticleSystem.Clear();
        _baseParticleSystem.Stop();
        _isActive = false;
        SetFireForce(1f);
    }

    /// <summary>
    /// Принимает значение от 0 до 1, где 0 это 0 HP а 1 это aekk HP
    /// </summary>
    public void SetFireForce(float value)
    {
        Debug.Log($"Set fire: {value}");
        value = Mathf.Clamp01(value);
        _value = value;
        _lastValue = value;
        
        if (_value > 0.5f)
        {
            if (_isActive)
            {
                _baseParticleSystem.Stop();
                _isActive = false;
            }
        }
        else
        {
            if (_isActive == false)
            {
                _isActive = true;
                _baseParticleSystem.Play();
            }
            
            var module = _fireEmmiterParticleSystem.emission;
            var t = Mathf.Clamp01(((_value  * 2f) - 0.2f) / 0.8f);
            var circleArea = 2f * Mathf.PI * _fireEmmiterParticleSystem.shape.radius;
            
            module.rateOverTime = Mathf.Lerp(_EmmitRange.x, _EmmitRange.y, t) * circleArea;
        }
    }

    private void OnValidate()
    {
        if (Mathf.Approximately(_value, _lastValue) == false)
        {
            _lastValue = _value;
            SetFireForce(_value);
        }
    }
}
