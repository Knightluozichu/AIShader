using UnityEngine;

namespace UIFramework.Core
{
    /// <summary>
    /// 基础UI组件接口，定义所有UI组件必须实现的基本功能
    /// </summary>
    public interface IUIComponent
    {
        /// <summary>
        /// UI组件的RectTransform组件
        /// </summary>
        RectTransform RectTransform { get; }

        /// <summary>
        /// UI组件的可见性
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// 初始化组件
        /// </summary>
        void Initialize();

        /// <summary>
        /// 显示组件
        /// </summary>
        void Show();

        /// <summary>
        /// 隐藏组件
        /// </summary>
        void Hide();

        /// <summary>
        /// 销毁组件
        /// </summary>
        void Destroy();
    }
}