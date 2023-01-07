using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.VFX
{
    [RequireComponent(typeof(Renderer))]
    [ExecuteInEditMode]
    public class PropertyBlockEditor : MonoBehaviour
    {
        public int orderInLayer;
        public Color color;

        public List<CustomFloat> CustomFloats = new();
        public List<CustomColor> CustomColors = new();
        public List<CustomTex> CustomTextures = new();

        public bool clearData;
        public SortingLayer layer;
        private MaterialPropertyBlock mpb;
        private Renderer renderer;

        private void Start()
        {
            Refresh();
        }

        private void OnValidate()
        {
            if (clearData)
            {
                clearData = false;
                renderer.SetPropertyBlock(null);
            }

            Refresh();
        }

        private void Init()
        {
            renderer = GetComponent<Renderer>();
            mpb = new MaterialPropertyBlock();
            if (!renderer.HasPropertyBlock()) renderer.SetPropertyBlock(mpb);
            renderer.GetPropertyBlock(mpb);
            if (renderer is SpriteRenderer)
            {
                color = renderer.GetComponent<SpriteRenderer>().color;
                orderInLayer = renderer.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }

        public void Refresh()
        {
            if (mpb == null)
                Init();
            renderer.sortingLayerID = layer.id;
            renderer.sortingOrder = orderInLayer;
            mpb.SetColor("_Color", color);
            for (var i = 0; i < CustomColors.Count; i++)
                mpb.SetColor(CustomColors[i].nameKey, CustomColors[i].color);

            for (var i = 0; i < CustomTextures.Count; i++)
                mpb.SetTexture(CustomTextures[i].nameKey, CustomTextures[i].tex);

            for (var i = 0; i < CustomFloats.Count; i++)
                mpb.SetFloat(CustomFloats[i].nameKey, CustomFloats[i].value);

            renderer.SetPropertyBlock(mpb);
        }

        [Serializable]
        public class CustomFloat
        {
            public string nameKey;
            public float value;
        }

        [Serializable]
        public class CustomColor
        {
            public string nameKey;
            public Color color = Color.white;
        }

        [Serializable]
        public class CustomTex
        {
            public string nameKey;
            public Texture tex;
        }
    }
}