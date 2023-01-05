using Global.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Loggers.Runtime.Headers
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "LoggerHeader", menuName = GlobalAssetsPaths.Logger + "Header")]
    public class LoggerHeader : ScriptableObject
    {
        [SerializeField] [HideLabel] private string _name;

        [SerializeField] private bool _isColored = true;

        [ShowIf("_isColored")] [SerializeField] [ColorPalette("LogHeader")] [HideLabel]
        private Color _color;

        public string Name => _name;
        public bool IsColored => _isColored;
        public string Color => ColorUtility.ToHtmlStringRGB(_color);
    }
}