using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer)), ExecuteInEditMode]
public class PropertyBlockEditor : MonoBehaviour
{
    private Renderer renderer;
    public SortingLayer layer;
    public int orderInLayer;
    public Color color;

    public List<CustomFloat> CustomFloats = new List<CustomFloat>();
    [System.Serializable]
    public class CustomFloat
    {
        public string nameKey;
        public float value;
    }
    public List<CustomColor> CustomColors = new List<CustomColor>();
    [System.Serializable]
    public class CustomColor
    {
        public string nameKey;
        public Color color = Color.white;
    }    
    public List<CustomTex> CustomTextures = new List<CustomTex>();
    [System.Serializable]
    public class CustomTex
    {
        public string nameKey;
        public Texture tex;
    }
    private MaterialPropertyBlock mpb;
    private void Start() => Refresh();

    public bool clearData;
    private void OnValidate()
    {
        if (clearData)
        {
            clearData = false;
            renderer.SetPropertyBlock(null);
        }
        Refresh();
    }

    void Init()
    {
        renderer = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        if (!renderer.HasPropertyBlock()) {
            renderer.SetPropertyBlock(mpb);
        }
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
        for (int i = 0; i < CustomColors.Count; i++)
            mpb.SetColor(CustomColors[i].nameKey, CustomColors[i].color);        
        
        for (int i = 0; i < CustomTextures.Count; i++)
            mpb.SetTexture(CustomTextures[i].nameKey, CustomTextures[i].tex);
        
        for (int i = 0; i < CustomFloats.Count; i++)
            mpb.SetFloat(CustomFloats[i].nameKey, CustomFloats[i].value);
        
        renderer.SetPropertyBlock(mpb);
    }
}

