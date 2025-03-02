using UnityEngine;
using UIFramework.Core;

namespace UIFramework.Binding
{
    /// <summary>
    /// 可绑定UI组件基类
    /// </summary>
    public abstract class BindableComponent : UIComponentBase
    {
        protected DataBindingContext BindingContext { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            BindingContext = new DataBindingContext();
            InitializeBindings();
        }

        /// <summary>
        /// 初始化数据绑定
        /// </summary>
        protected abstract void InitializeBindings();

        /// <summary>
        /// 设置绑定上下文
        /// </summary>
        /// <param name="context">绑定上下文</param>
        public void SetBindingContext(DataBindingContext context)
        {
            BindingContext = context;
            OnBindingContextChanged();
        }

        /// <summary>
        /// 绑定上下文变更时调用
        /// </summary>
        protected virtual void OnBindingContextChanged() { }
    }
}