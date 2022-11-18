using UnityEngine;

namespace GamePlay.Level.Environment.Tools
{
    [CreateAssetMenu(fileName = "GroundColorPalette", menuName = "GamePlay/Tools/GroundColorPalette")]
    public class GroundColorPalette : ScriptableObject
    {
        [SerializeField] private Color _color0;
        [SerializeField] private Color _color1;
        [SerializeField] private Color _color2;
        [SerializeField] private Color _color3;

        public Color[] GetColors()
        {
            return new[]
            {
                _color0,
                _color1,
                _color2,
                _color3
            };
        }
    }
}