using UnityEngine;

namespace Project.Utilities{

    public class ToolTipManager : MonoBehaviour
    {
        [SerializeField] private ToolTip TooltipInstance;
        private static ToolTip tooltipInstance;

        public void Initialize()
        {
            if (TooltipInstance == null)
            {
                Debug.LogError("TooltipInstance is not assigned in the inspector!");
                return;
            }

            if (tooltipInstance == null)
            {
                tooltipInstance = TooltipInstance;
                tooltipInstance.Hide();
            }
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
