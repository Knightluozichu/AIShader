using UnityEngine;
using UIFramework.Core;
using System.Linq;

namespace UIFramework.Layout
{
    /// <summary>
    /// 弹性布局策略
    /// </summary>
    public class FlexLayoutStrategy : LayoutBase
    {
        public enum FlexDirection
        {
            Horizontal,
            Vertical
        }

        private FlexDirection _direction = FlexDirection.Horizontal;
        private bool _reverse = false;
        private bool _wrap = false;

        public FlexDirection Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                OnLayoutPropertyChanged();
            }
        }

        public bool Reverse
        {
            get => _reverse;
            set
            {
                _reverse = value;
                OnLayoutPropertyChanged();
            }
        }

        public bool Wrap
        {
            get => _wrap;
            set
            {
                _wrap = value;
                OnLayoutPropertyChanged();
            }
        }

        public override void CalculateLayout(IUIContainer container)
        {
            var children = container.GetChildren().ToList();
            if (children.Count == 0) return;

            var containerRect = (container as UIComponentBase)?.RectTransform;
            if (containerRect == null) return;

            float currentX = padding.x;
            float currentY = -padding.y;
            float maxHeight = 0;
            float maxWidth = 0;

            foreach (var child in children)
            {
                if (child.RectTransform == null) continue;

                Vector2 childSize = child.RectTransform.sizeDelta;

                if (_direction == FlexDirection.Horizontal)
                {
                    if (_wrap && currentX + childSize.x > containerRect.rect.width)
                    {
                        currentX = padding.x;
                        currentY -= maxHeight + spacing.y;
                        maxHeight = 0;
                    }

                    child.RectTransform.anchorMin = new Vector2(0, 1);
                    child.RectTransform.anchorMax = new Vector2(0, 1);
                    child.RectTransform.anchoredPosition = new Vector2(
                        _reverse ? containerRect.rect.width - currentX - childSize.x : currentX,
                        currentY
                    );

                    currentX += childSize.x + spacing.x;
                    maxHeight = Mathf.Max(maxHeight, childSize.y);
                }
                else
                {
                    if (_wrap && -currentY + childSize.y > containerRect.rect.height)
                    {
                        currentY = -padding.y;
                        currentX += maxWidth + spacing.x;
                        maxWidth = 0;
                    }

                    child.RectTransform.anchorMin = new Vector2(0, 1);
                    child.RectTransform.anchorMax = new Vector2(0, 1);
                    child.RectTransform.anchoredPosition = new Vector2(
                        currentX,
                        _reverse ? -currentY : currentY
                    );

                    currentY -= childSize.y + spacing.y;
                    maxWidth = Mathf.Max(maxWidth, childSize.x);
                }
            }
        }

        public override void ApplyLayout(IUIContainer container)
        {
            CalculateLayout(container);
        }
    }
}