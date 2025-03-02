using UnityEngine;
using UIFramework.Core;
using System.Linq;

namespace UIFramework.Layout
{
    /// <summary>
    /// 网格布局策略
    /// </summary>
    public class GridLayoutStrategy : LayoutBase
    {
        private Vector2 _cellSize = new Vector2(100, 100);
        private int _columns = 1;
        private bool _expandHorizontal = true;

        public Vector2 CellSize
        {
            get => _cellSize;
            set
            {
                _cellSize = value;
                OnLayoutPropertyChanged();
            }
        }

        public int Columns
        {
            get => _columns;
            set
            {
                _columns = Mathf.Max(1, value);
                OnLayoutPropertyChanged();
            }
        }

        public override void CalculateLayout(IUIContainer container)
        {
            var children = container.GetChildren().ToList();
            if (children.Count == 0) return;

            int row = 0;
            int col = 0;

            foreach (var child in children)
            {
                if (child.RectTransform == null) continue;

                float x = padding.x + (col * (_cellSize.x + spacing.x));
                float y = -padding.y - (row * (_cellSize.y + spacing.y));

                child.RectTransform.anchorMin = new Vector2(0, 1);
                child.RectTransform.anchorMax = new Vector2(0, 1);
                child.RectTransform.sizeDelta = _cellSize;
                child.RectTransform.anchoredPosition = new Vector2(x, y);

                col++;
                if (col >= _columns)
                {
                    col = 0;
                    row++;
                }
            }
        }

        public override void ApplyLayout(IUIContainer container)
        {
            CalculateLayout(container);
        }
    }
}