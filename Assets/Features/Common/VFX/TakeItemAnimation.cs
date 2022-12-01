using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeItemAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _icon;
   [SerializeField] private TMP_Text _tmp;

   public void StartAnimation(Sprite icon, int add_count)
   {
       _icon.sprite = icon;
       _tmp.text = "+" + add_count;
       gameObject.SetActive(true);
   }

   public void Deactivate()
   {
       gameObject.SetActive(false);
   }
}
