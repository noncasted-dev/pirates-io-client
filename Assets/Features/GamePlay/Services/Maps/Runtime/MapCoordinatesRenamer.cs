using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace GamePlay.Services.Maps.Runtime
{
    [DisallowMultipleComponent]
    public class MapCoordinatesRenamer : MonoBehaviour
    {
        [SerializeField] private string _sign = "S";

        [Button]
        private void Rename()
        {
            var texts = GetComponentsInChildren<TMP_Text>();

            for (var i = 0; i < texts.Length; i++)
            {
                texts[i].text = $"{i:000}°{_sign}";

#if UNITY_EDITOR
                Undo.RecordObject(texts[i].gameObject, "Assign text");
                EditorUtility.SetDirty(texts[i]);
                EditorSceneManager.MarkSceneDirty(texts[i].gameObject.scene);
#endif
            }
        }
    }
}