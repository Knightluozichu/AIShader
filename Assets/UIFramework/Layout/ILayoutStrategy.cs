using UIFramework.Core;

namespace UIFramework.Layout
{
    /// <summary>
    /// 布局策略接口
    /// </summary>
    public interface ILayoutStrategy
    {
        /// <summary>
        /// 计算布局
        /// </summary>
        /// <param name="container">UI容器</param>
        void CalculateLayout(IUIContainer container);

        /// <summary>
        /// 应用布局
        /// </summary>
        /// <param name="container">UI容器</param>
        void ApplyLayout(IUIContainer container);
    }
}