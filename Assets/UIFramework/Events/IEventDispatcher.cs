using System;

namespace UIFramework.Events
{
    /// <summary>
    /// 事件分发器接口
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// 添加事件监听器
        /// </summary>
        /// <typeparam name="T">事件参数类型</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">事件处理器</param>
        void AddEventListener<T>(string eventType, Action<T> handler) where T : UIEventArgs;

        /// <summary>
        /// 移除事件监听器
        /// </summary>
        /// <typeparam name="T">事件参数类型</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">事件处理器</param>
        void RemoveEventListener<T>(string eventType, Action<T> handler) where T : UIEventArgs;

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <typeparam name="T">事件参数类型</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventArgs">事件参数</param>
        void DispatchEvent<T>(string eventType, T eventArgs) where T : UIEventArgs;

        /// <summary>
        /// 检查是否有特定类型的事件监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <returns>是否存在监听器</returns>
        bool HasEventListener(string eventType);
    }
}