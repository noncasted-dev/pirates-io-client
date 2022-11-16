#region

using System;
using System.Collections.Generic;
using Common.EditableScriptableObjects.Attributes;
using UnityEditor;
using UnityEngine;

#endregion

#if UNITY_EDITOR
namespace Common.EditableScriptableObjects.Editor
{
    /// <summary>
    ///     Draws the property field for any field marked with ExpandableAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(EditableObjectAttribute), true)]
    public class EditableObjectAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var totalHeight = 0.0f;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.objectReferenceValue == null)
                return totalHeight;

            if (!property.isExpanded)
                return totalHeight;

            var targetObject = new SerializedObject(property.objectReferenceValue);

            var field = targetObject.GetIterator();

            field.NextVisible(true);

            while (field.NextVisible(false))
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;

            totalHeight += _innerSpacing * 2;
            totalHeight += _outerSpacing * 2;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldRect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight
            };

            EditorGUI.PropertyField(fieldRect, property, label, true);

            if (property.objectReferenceValue == null)
                return;

            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

            if (!property.isExpanded)
                return;

            var targetObject = new SerializedObject(property.objectReferenceValue);

            #region Format Field Rects

            var propertyRects = new List<Rect>();
            var marchingRect = new Rect(fieldRect);

            var bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 14;
            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                                                               + _outerSpacing;

            var field = targetObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += _innerSpacing + _outerSpacing;

            while (field.NextVisible(false))
            {
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += _innerSpacing;

            bodyRect.yMax = marchingRect.yMax;

            #endregion

            DrawBackground(bodyRect);

            #region Draw Fields

            EditorGUI.indentLevel++;

            var index = 0;
            field = targetObject.GetIterator();
            field.NextVisible(true);

            while (field.NextVisible(false))
            {
                try
                {
                    EditorGUI.PropertyField(propertyRects[index], field, true);
                }
                catch (StackOverflowException)
                {
                    field.objectReferenceValue = null;
                    Debug.LogError("Detected self-nesting cause a StackOverflowException, avoid using the same " +
                                   "object inside a nested structure.");
                }

                index++;
            }

            targetObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;

            #endregion
        }

        /// <summary>
        ///     Draws the Background
        /// </summary>
        /// <param name="rect">The Rect where the background is drawn.</param>
        private void DrawBackground(Rect rect)
        {
            switch (_backgroundStyle)
            {
                case BackgroundStyles.HelpBox:
                    EditorGUI.HelpBox(rect, "", MessageType.None);
                    break;

                case BackgroundStyles.Darken:
                    EditorGUI.DrawRect(rect, DARKEN_COLOUR);
                    break;

                case BackgroundStyles.Lighten:
                    EditorGUI.DrawRect(rect, LIGHTEN_COLOUR);
                    break;
                case BackgroundStyles.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        // Use the following area to change the style of the expandable ScriptableObject drawers;

        #region Style Setup

        private enum BackgroundStyles
        {
            None,
            HelpBox,
            Darken,
            Lighten
        }

        /// <summary>
        ///     The spacing on the inside of the background rect.
        /// </summary>
        private const float _innerSpacing = 6.0f;

        /// <summary>
        ///     The spacing on the outside of the background rect.
        /// </summary>
        private const float _outerSpacing = 4.0f;

        /// <summary>
        ///     The style the background uses.
        /// </summary>
        private const BackgroundStyles _backgroundStyle = BackgroundStyles.HelpBox;

        /// <summary>
        ///     The colour that is used to darken the background.
        /// </summary>
        private static readonly Color DARKEN_COLOUR = new(0.0f, 0.0f, 0.0f, 0.2f);

        /// <summary>
        ///     The colour that is used to lighten the background.
        /// </summary>
        private static readonly Color LIGHTEN_COLOUR = new(1.0f, 1.0f, 1.0f, 0.2f);

        #endregion
    }
}
#endif