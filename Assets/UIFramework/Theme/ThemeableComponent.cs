using UnityEngine;
using UIFramework.Core;
using UIFramework.Events;

namespace UIFramework.Theme
{
    /// <summary>
    /// 可主题化组件基类
    /// </summary>
    public abstract class ThemeableComponent : UIComponentBase, IThemeable
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            UIEventManager.Instance.AddEventListener<ThemeManager.ThemeChangedEventArgs>(
                "ThemeChanged", OnThemeChanged);
            
            // 应用当前主题
            var currentTheme = ThemeManager.Instance.GetCurrentTheme();
            if (currentTheme != null)
            {
                ApplyTheme(currentTheme);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UIEventManager.Instance.RemoveEventListener<ThemeManager.ThemeChangedEventArgs>(
                "ThemeChanged", OnThemeChanged);
        }

        private void OnThemeChanged(ThemeManager.ThemeChangedEventArgs args)
        {
            ApplyTheme(args.Theme);
        }

        public abstract void ApplyTheme(UITheme theme);
    }
}