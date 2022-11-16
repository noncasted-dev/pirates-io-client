using UnityEngine;
using UnityEngine.UI;

namespace Common.Artwork.UI.UI_pack.Scripts
{
    public class FillValueNumber : MonoBehaviour
    {
        public Image TargetImage;

        // Update is called once per frame
        private void Update()
        {
            var amount = TargetImage.fillAmount * 100;
            gameObject.GetComponent<Text>().text = amount.ToString("F0");
        }
    }
}