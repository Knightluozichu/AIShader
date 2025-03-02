using UnityEngine;
using System.Collections.Generic;

namespace UIFramework.Theme
{
    /// <summary>
    /// UI主题定义
    /// </summary>
    [CreateAssetMenu(fileName = "NewUITheme", menuName = "UIFramework/Theme")]
    public class UITheme : ScriptableObject
    {
        [System.Serializable]
        public class ColorScheme
        {
            public Color primary = Color.white;
            public Color secondary = Color.gray;
            public Color accent = Color.blue;
            public Color background = Color.black;
            public Color text = Color.white;
        }

        [System.Serializable]
        public class FontScheme
        {
            public Font defaultFont;
            public Font headerFont;
            public Font bodyFont;
        }

        public string themeName = "Default Theme";
        public ColorScheme colors = new ColorScheme();
        public FontScheme fonts = new FontScheme();

        [Header("Spacing")]
        public float defaultSpacing = 10f;
        public float defaultPadding = 10f;

        [Header("Sizes")]
        public float buttonHeight = 40f;
        public float inputFieldHeight = 30f;
        public Vector2 iconSize = new Vector2(32, 32);

        private Dictionary<string, object> _customProperties = new Dictionary<string, object>();

        public T GetCustomProperty<T>(string key, T defaultValue = default)
        {
            if (_customProperties.TryGetValue(key, out object value))
            {
                if (value is T typedValue)
                {
                    return typedValue;
                }
            }
            return defaultValue;
        }

        public void SetCustomProperty<T>(string key, T value)
        {
            _customProperties[key] = value;
        }
    }
}