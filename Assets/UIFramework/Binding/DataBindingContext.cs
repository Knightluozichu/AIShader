using System;
using System.Collections.Generic;
using UIFramework.Utils;

namespace UIFramework.Binding
{
    /// <summary>
    /// 数据绑定上下文
    /// </summary>
    public class DataBindingContext
    {
        private readonly Dictionary<string, object> _bindingProperties = new Dictionary<string, object>();
        private readonly Dictionary<string, List<Action<object>>> _bindingCallbacks = new Dictionary<string, List<Action<object>>>();

        /// <summary>
        /// 注册可绑定属性
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="initialValue">初始值</param>
        /// <returns>可绑定属性对象</returns>
        public BindableProperty<T> RegisterProperty<T>(string propertyName, T initialValue = default)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Property name cannot be null or empty");
            }

            if (_bindingProperties.ContainsKey(propertyName))
            {
                UILogger.LogError($"Property {propertyName} is already registered");
                return null;
            }

            var property = new BindableProperty<T>(initialValue);
            _bindingProperties[propertyName] = property;

            property.OnValueChanged += value =>
            {
                if (_bindingCallbacks.TryGetValue(propertyName, out var callbacks))
                {
                    foreach (var callback in callbacks)
                    {
                        try
                        {
                            callback.Invoke(value);
                        }
                        catch (Exception ex)
                        {
                            UILogger.LogError($"Error in binding callback for {propertyName}: {ex.Message}");
                        }
                    }
                }
            };

            return property;
        }

        /// <summary>
        /// 获取绑定属性
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <returns>可绑定属性对象</returns>
        public BindableProperty<T> GetProperty<T>(string propertyName)
        {
            if (_bindingProperties.TryGetValue(propertyName, out object property))
            {
                if (property is BindableProperty<T> typedProperty)
                {
                    return typedProperty;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加绑定回调
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="callback">回调方法</param>
        public void AddBindingCallback(string propertyName, Action<object> callback)
        {
            if (!_bindingCallbacks.ContainsKey(propertyName))
            {
                _bindingCallbacks[propertyName] = new List<Action<object>>();
            }
            _bindingCallbacks[propertyName].Add(callback);
        }

        /// <summary>
        /// 移除绑定回调
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="callback">回调方法</param>
        public void RemoveBindingCallback(string propertyName, Action<object> callback)
        {
            if (_bindingCallbacks.TryGetValue(propertyName, out var callbacks))
            {
                callbacks.Remove(callback);
            }
        }
    }
}