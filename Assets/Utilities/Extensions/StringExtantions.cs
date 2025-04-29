using UnityEngine;

namespace Project.Utilities.Extantions{
    public static class StringExtantions{
        public static Color ToColor(this string hex)
        {
            hex = hex.Replace("#", "");

            if (hex.Length != 6 && hex.Length != 8)
            {
                Debug.LogError($"HEX-string '{hex}' must have lenght of 6 (RRGGBB) or 8 (RRGGBBAA) symbols!");
                return Color.magenta;
            }

            try
            {
                byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                byte a = (hex.Length == 8)
                    ? byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)
                    : (byte)255;
                return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error while parcing HEX '{hex}': {e.Message}");
                return Color.magenta;
            }
        }
        
        public static T LoadResource<T>(this string path) where T: Object{
            return Resources.Load<T>(path);
        }
    }
}
