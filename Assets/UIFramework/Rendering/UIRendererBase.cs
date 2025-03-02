using UnityEngine;
using UnityEngine.UI;
using UIFramework.Core;
using UIFramework.Utils;

namespace UIFramework.Rendering
{
    /// <summary>
    /// UI渲染器基类
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public abstract class UIRendererBase : UIComponentBase, IUIRenderer
    {
        protected CanvasRenderer _canvasRenderer;
        protected Material _material;
        protected bool _isDirty = true;
        protected bool _isEnabled = true;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    if (_canvasRenderer != null)
                    {
                        _canvasRenderer.cull = !value;
                    }
                }
            }
        }

        public int SortingOrder
        {
            get => _canvasRenderer != null ? _canvasRenderer.absoluteDepth : 0;
            set
            {
                if (_canvasRenderer != null)
                {
                    Canvas canvas = GetComponentInParent<Canvas>();
                    if (canvas != null)
                    {
                        // 使用 SetOrder 来设置渲染顺序
                        canvas.sortingOrder = value;
                    }
                }
            }
        }

        protected virtual void Awake()
        {
            _canvasRenderer = GetComponent<CanvasRenderer>();
            if (_canvasRenderer == null)
            {
                UILogger.LogError("CanvasRenderer component is required");
                return;
            }
        }

        public virtual void SetMaterial(Material material)
        {
            if (_material != material)
            {
                _material = material;
                if (_canvasRenderer != null)
                {
                    _canvasRenderer.SetMaterial(_material, null);
                }
                _isDirty = true;
            }
        }

        public Material GetMaterial()
        {
            return _material;
        }

        public abstract void UpdateRenderData();

        public virtual void Render()
        {
            if (!_isEnabled || _canvasRenderer == null) return;

            if (_isDirty)
            {
                UpdateRenderData();
                _isDirty = false;
            }
        }

        protected virtual void OnEnable()
        {
            _isDirty = true;
            if (_canvasRenderer != null)
            {
                _canvasRenderer.cull = !_isEnabled;
            }
        }

        protected virtual void OnDisable()
        {
            if (_canvasRenderer != null)
            {
                _canvasRenderer.cull = true;
            }
        }
    }
}