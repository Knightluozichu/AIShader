namespace UIFramework.Theme
{
    /// <summary>
    /// 可主题化接口
    /// </summary>
    public interface IThemeable
    {
        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="theme">主题对象</param>
        void ApplyTheme(UITheme theme);
    }
}