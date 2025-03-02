using UnityEngine;
using UIFramework.Core;

namespace UIFramework.Layout
{
    /// <summary>
    /// 布局基类
    /// </summary>
    public abstract class LayoutBase : ILayoutStrategy
    {
        protected Vector2 spacing = Vector2.zero;
        protected Vector2 padding = Vector2.zero;

        public Vector2 Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                OnLayoutPropertyChanged();
            }
        }

        public Vector2 Padding
        {
            get => padding;
            set
            {
                padding = value;
                OnLayoutPropertyChanged();
            }
        }

        public abstract void CalculateLayout(IUIContainer container);
        public abstract void ApplyLayout(IUIContainer container);

        protected virtual void OnLayoutPropertyChanged() { }
    }
}