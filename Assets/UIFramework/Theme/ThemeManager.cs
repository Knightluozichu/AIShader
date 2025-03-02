using UnityEngine;
using System;
using System.Collections.Generic;
using UIFramework.Events;
using UIFramework.Utils;

namespace UIFramework.Theme
{
    /// <summary>
    /// 主题管理器实现
    /// </summary>
    public class ThemeManager : MonoBehaviour, IThemeManager
    {
        private static ThemeManager _instance;
        public static ThemeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("ThemeManager");
                    _instance = go.AddComponent<ThemeManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private readonly Dictionary<string, UITheme> _themes = new Dictionary<string, UITheme>();
        private UITheme _currentTheme;

        public class ThemeChangedEventArgs : UIEventArgs
        {
            public string ThemeName { get; private set; }
            public UITheme Theme { get; private set; }

            public ThemeChangedEventArgs(object source, string themeName, UITheme theme) 
                : base(source)
            {
                ThemeName = themeName;
                Theme = theme;
            }
        }

        public void RegisterTheme(string themeName, UITheme theme)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                UILogger.LogError("Theme name cannot be null or empty");
                return;
            }

            if (theme == null)
            {
                UILogger.LogError($"Theme object is null for theme: {themeName}");
                return;
            }

            _themes[themeName] = theme;
            UILogger.Log($"Registered theme: {themeName}");
        }

        public void ApplyTheme(string themeName)
        {
            if (!_themes.TryGetValue(themeName, out UITheme theme))
            {
                UILogger.LogError($"Theme not found: {themeName}");
                return;
            }

            _currentTheme = theme;
            UILogger.Log($"Applied theme: {themeName}");

            // 分发主题变更事件
            UIEventManager.Instance.DispatchEvent("ThemeChanged", 
                new ThemeChangedEventArgs(this, themeName, theme));
        }

        public void UpdateTheme(string themeName, Action<UITheme> updateAction)
        {
            if (!_themes.TryGetValue(themeName, out UITheme theme))
            {
                UILogger.LogError($"Theme not found: {themeName}");
                return;
            }

            updateAction?.Invoke(theme);
            
            if (_currentTheme == theme)
            {
                // 如果更新的是当前主题，触发主题变更事件
                UIEventManager.Instance.DispatchEvent("ThemeChanged", 
                    new ThemeChangedEventArgs(this, themeName, theme));
            }
        }

        public UITheme GetCurrentTheme()
        {
            return _currentTheme;
        }
    }
}