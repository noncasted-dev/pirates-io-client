using System;
using System.Collections.Generic;
using System.Linq;
using Common.NestedScriptableObjects.Attributes;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.NestedScriptableObjects.Editor
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public class NestedScriptableObjectListAttributeDrawer<TList, T>
        : OdinAttributeDrawer<NestedScriptableObjectListAttribute,
            TList> where TList : List<T> where T : ScriptableObject
    {
        private const int _maxSearchIterations = 3;

        private Object Parent => GetParent();

        private Object GetParent()
        {
            var parent = Property.Parent;
            var iterationsCount = 0;

            while (true)
            {
                if (iterationsCount > _maxSearchIterations)
                    throw new ArgumentException("No suitable parent found");

                try
                {
                    parent = parent.Parent;
                    var _ = (Object)parent.ValueEntry.WeakSmartValue;
                }
                catch (Exception e)
                {
                    iterationsCount++;
                    continue;
                }

                return (Object)parent.ValueEntry.WeakSmartValue;
            }
        }

        protected override void Initialize()
        {
            Attribute.Type = typeof(T);
            base.Initialize();
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);

            Remove();
            Add();
        }

        private void Remove()
        {
            if (Attribute.ObjectsToRemove.Count <= 0)
                return;

            var objectToRemove = Attribute.ObjectsToRemove[0];
            Attribute.OnRemoved(objectToRemove);

            if (ValueEntry.SmartValue.Contains(objectToRemove) == false)
                return;

            AssetDatabase.Refresh();
            ValueEntry.SmartValue.Remove((T)objectToRemove);

            if (objectToRemove == null)
                return;

            if (IsChild(objectToRemove) == true)
                Object.DestroyImmediate(objectToRemove, true);

            if (Application.isPlaying == true)
                return;

            AssetDatabase.ForceReserializeAssets(new[] { AssetDatabase.GetAssetPath(Parent) });
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void Add()
        {
            if (Attribute.ObjectsToCreate.Count <= 0)
                return;

            var objectToCreate = Attribute.ObjectsToCreate[0];

            Attribute.OnCreated(objectToCreate);

            var parent = GetParent();
            var parentName = parent.name.Replace("LogSettings_", "");
            var objectName = objectToCreate.GetType().Name;

            if (objectName.Contains("EmptyEntry") == true)
                return;

            objectToCreate.name = $"{objectName}_{parentName}";

            AssetDatabase.AddObjectToAsset(objectToCreate, Parent);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private bool IsChild(Object target)
        {
            var parent = GetParent();
            var parentName = parent.name.Replace("LogSettings_", "");

            if (target.name.Contains(parentName) == false)
                return false;

            return true;
        }
    }
}