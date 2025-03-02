using System;

namespace UIFramework.Events
{
    /// <summary>
    /// UI事件参数基类
    /// </summary>
    public class UIEventArgs : EventArgs
    {
        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        public float Timestamp { get; private set; }

        /// <summary>
        /// 事件源
        /// </summary>
        public object Source { get; private set; }

        public UIEventArgs(object source)
        {
            Timestamp = UnityEngine.Time.time;
            Source = source;
        }
    }
}