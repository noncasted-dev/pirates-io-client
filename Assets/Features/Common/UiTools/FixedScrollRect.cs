using UnityEngine.UI;

namespace Common.UiTools
{
    public class FixedScrollRect : ScrollRect
    {
        protected override void LateUpdate()
        {
            base.LateUpdate();
            
            if (verticalScrollbar)
                verticalScrollbar.size = 0f;
        }

        public override void Rebuild(CanvasUpdate executing)
        {
            base.Rebuild(executing);
            
            if (verticalScrollbar == true)
                verticalScrollbar.size = 0f;
        }
    }
}