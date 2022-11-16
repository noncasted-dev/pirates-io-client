using UnityEngine.UI;

namespace Common.Structs
{
    public static class ColorUtils
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }

        public static void SetAlphaZero(this Image image)
        {
            var color = image.color;
            color.a = 0f;
            image.color = color;
        }

        public static void SetAlphaOne(this Image image)
        {
            var color = image.color;
            color.a = 1f;
            image.color = color;
        }
    }
}