using System;

namespace UIFramework.Binding
{
    /// <summary>
    /// 可绑定接口
    /// </summary>
    /// <typeparam name="T">绑定值的类型</typeparam>
    public interface IBindable<T>
    {
        /// <summary>
        /// 绑定的值
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// 值变更事件
        /// </summary>
        event Action<T> OnValueChanged;
    }
}