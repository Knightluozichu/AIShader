using System.Collections.Generic;

namespace UIFramework.Core
{
    /// <summary>
    /// 容器接口，用于管理子组件
    /// </summary>
    public interface IUIContainer : IUIComponent
    {
        /// <summary>
        /// 添加子组件
        /// </summary>
        /// <param name="child">要添加的子组件</param>
        void AddChild(IUIComponent child);

        /// <summary>
        /// 移除子组件
        /// </summary>
        /// <param name="child">要移除的子组件</param>
        void RemoveChild(IUIComponent child);

        /// <summary>
        /// 获取指定索引的子组件
        /// </summary>
        /// <param name="index">子组件索引</param>
        /// <returns>子组件</returns>
        IUIComponent GetChild(int index);

        /// <summary>
        /// 获取所有子组件
        /// </summary>
        /// <returns>子组件集合</returns>
        IEnumerable<IUIComponent> GetChildren();
    }
}