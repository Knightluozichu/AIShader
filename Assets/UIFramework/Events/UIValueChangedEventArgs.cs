namespace UIFramework.Events.Common
{
    /// <summary>
    /// UI值变更事件参数
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    public class UIValueChangedEventArgs<T> : UIEventArgs
    {
        /// <summary>
        /// 旧值
        /// </summary>
        public T OldValue { get; private set; }

        /// <summary>
        /// 新值
        /// </summary>
        public T NewValue { get; private set; }

        public UIValueChangedEventArgs(object source, T oldValue, T newValue) 
            : base(source)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}