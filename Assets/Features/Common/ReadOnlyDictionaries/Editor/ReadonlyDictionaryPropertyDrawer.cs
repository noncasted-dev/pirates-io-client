using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Common.ReadOnlyDictionaries.Editor
{
    public abstract class ReadonlyDictionaryPropertyDrawer : PropertyDrawer
    {
        private const string _keysName = "_keys";
        private const string _valuesName = "_values";
        private const float _indentWidth = 15f;

        private static readonly GUIContent tmp = new();

        protected abstract bool IsCollapsed { get; }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);

            var key = property.FindPropertyRelative(_keysName);
            var value = property.FindPropertyRelative(_valuesName);

            var labelPosition = position;
            labelPosition.height = EditorGUIUtility.singleLineHeight;

            if (IsCollapsed == true)
            {
                EditorGUI.PropertyField(labelPosition, property, label, IsCollapsed);

                if (property.isExpanded == false)
                {
                    EditorGUI.EndProperty();
                    return;
                }

                EditorGUI.indentLevel++;
            }
            else
            {
                EditorGUI.LabelField(labelPosition, label);
            }

            var linePosition = position;
            linePosition.y += EditorGUIUtility.singleLineHeight;

            foreach (var entry in EnumerateEntries(key, value))
            {
                var keyProperty = entry.Key;
                var valueProperty = entry.Value;

                var lineHeight = DrawElement(keyProperty, valueProperty, linePosition);

                linePosition.y += lineHeight;
            }

            if (IsCollapsed == true)
                EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }

        private static float DrawElement(
            SerializedProperty key,
            SerializedProperty value,
            Rect position)
        {
            var width = EditorGUIUtility.labelWidth;
            var WidthRelative = width / position.width;

            var keyHeight = EditorGUI.GetPropertyHeight(key);

            var keyPosition = position;
            keyPosition.height = keyHeight;
            keyPosition.width = width - _indentWidth;

            EditorGUIUtility.labelWidth = keyPosition.width * WidthRelative;

            GUI.enabled = false;
            EditorGUI.PropertyField(keyPosition, key, AssignFieldText(""), true);
            GUI.enabled = true;

            var valueHeight = EditorGUI.GetPropertyHeight(value);
            var valuePosition = position;
            valuePosition.height = valueHeight;
            valuePosition.xMin += width;

            EditorGUIUtility.labelWidth = valuePosition.width * WidthRelative;

            EditorGUI.indentLevel--;
            EditorGUI.PropertyField(valuePosition, value, AssignFieldText(""), true);
            EditorGUI.indentLevel++;

            EditorGUIUtility.labelWidth = width;

            return Mathf.Max(keyHeight, valueHeight);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;

            if (IsCollapsed && property.isExpanded == false)
                return height;

            var keys = property.FindPropertyRelative(_keysName);
            var values = property.FindPropertyRelative(_valuesName);

            foreach (var _entry in EnumerateEntries(keys, values))
            {
                var key = _entry.Key;
                var value = _entry.Value;

                var keyHeight = EditorGUI.GetPropertyHeight(key);
                var valueHeight = EditorGUI.GetPropertyHeight(value);
                var lineHeight = Mathf.Max(keyHeight, valueHeight);

                height += lineHeight;
            }

            return height;
        }

        private static GUIContent AssignFieldText(string text)
        {
            tmp.text = text;
            return tmp;
        }

        private static IEnumerable<EnumerationEntry> EnumerateEntries(
            SerializedProperty keys,
            SerializedProperty values)
        {
            if (keys.arraySize <= 0)
                yield break;

            var key = keys.GetArrayElementAtIndex(0);
            var value = values.GetArrayElementAtIndex(0);
            var end = keys.GetEndProperty();

            do
            {
                yield return new EnumerationEntry(key, value);
            } while (key.Next(false) == true
                     && value.Next(false) == true
                     && SerializedProperty.EqualContents(key, end) == false);
        }

        private readonly struct EnumerationEntry
        {
            public readonly SerializedProperty Key;
            public readonly SerializedProperty Value;

            public EnumerationEntry(SerializedProperty key, SerializedProperty value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}