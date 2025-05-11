using UnityEngine;

namespace Project.Utilities{

    public class ToolTipManager : MonoBehaviour
    {
        public ToolTip TooltipInstance;
        private static ToolTip tooltipInstance;

        public void Initialize()
        {
            if (tooltipInstance == null && TooltipInstance != null)
            {
                tooltipInstance = TooltipInstance;

            }
            tooltipInstance.Hide();
        }

        public static void ShowTooltip(string header, string message)
        {
            if (tooltipInstance != null)
            {
                tooltipInstance.Header.text = header;
                tooltipInstance.Message.text = message;

                tooltipInstance.Show();
            }
        }

        public static void HideTooltip()
        {
            if (tooltipInstance != null)
            {
                tooltipInstance.Hide();
            }
        }

    }

}
