using System;

namespace UIFramework.Theme
{
    /// <summary>
    /// 主题管理器接口
    /// </summary>
    public interface IThemeManager
    {
        /// <summary>
        /// 注册主题
        /// </summary>
        /// <param name="themeName">主题名称</param>
        /// <param name="theme">主题对象</param>
        void RegisterTheme(string themeName, UITheme theme);

        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="themeName">主题名称</param>
        void ApplyTheme(string themeName);

        /// <summary>
        /// 更新主题
        /// </summary>
        /// <param name="themeName">主题名称</param>
        /// <param name="updateAction">更新操作</param>
        void UpdateTheme(string themeName, Action<UITheme> updateAction);

        /// <summary>
        /// 获取当前主题
        /// </summary>
        /// <returns>当前主题对象</returns>
        UITheme GetCurrentTheme();
    }
}