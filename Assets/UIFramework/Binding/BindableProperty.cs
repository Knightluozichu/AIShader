using System;
using UIFramework.Utils;
using System.Collections.Generic; // 添加这个命名空间引用

namespace UIFramework.Binding
{
    /// <summary>
    /// 可绑定属性实现
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public class BindableProperty<T> : IBindable<T>
    {
        private T _value;
        private readonly bool _notifyOnSameValue;

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (!_notifyOnSameValue && EqualityComparer<T>.Default.Equals(_value, value))
                {
                    return;
                }

                T oldValue = _value;
                _value = value;
                
                try
                {
                    OnValueChanged?.Invoke(_value);
                }
                catch (Exception ex)
                {
                    UILogger.LogError($"Error in BindableProperty value change handler: {ex.Message}");
                }
            }
        }

        public BindableProperty(T initialValue = default, bool notifyOnSameValue = false)
        {
            _value = initialValue;
            _notifyOnSameValue = notifyOnSameValue;
        }
    }
}