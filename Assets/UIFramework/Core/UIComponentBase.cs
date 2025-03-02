using UnityEngine;

namespace UIFramework.Core
{
    /// <summary>
    /// UI组件基类，实现IUIComponent接口的基本功能
    /// </summary>
    public abstract class UIComponentBase : MonoBehaviour, IUIComponent
    {
        private RectTransform _rectTransform;
        private bool _isInitialized;
        private bool _isVisible;

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                    if (_rectTransform == null)
                    {
                        _rectTransform = gameObject.AddComponent<RectTransform>();
                    }
                }
                return _rectTransform;
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    if (_isVisible)
                    {
                        OnShow();
                    }
                    else
                    {
                        OnHide();
                    }
                }
            }
        }

        // 添加虚拟的 Awake 方法
        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            if (_rectTransform == null)
            {
                _rectTransform = gameObject.AddComponent<RectTransform>();
            }
        }

        // 添加虚拟的 OnEnable 方法
        protected virtual void OnEnable()
        {
            if (!_isInitialized)
            {
                Initialize();
            }
        }

        // 添加虚拟的 OnDisable 方法
        protected virtual void OnDisable()
        {
            if (IsVisible)
            {
                IsVisible = false;
            }
        }

        public virtual void Initialize()
        {
            if (_isInitialized) return;
            
            _isInitialized = true;
            OnInitialize();
        }

        public void Show()
        {
            if (!_isInitialized)
            {
                Initialize();
            }
            gameObject.SetActive(true);
            IsVisible = true;
        }

        public void Hide()
        {
            IsVisible = false;
            gameObject.SetActive(false);
        }

        public virtual void Destroy()
        {
            OnDestroy();
            Destroy(gameObject);
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
        protected virtual void OnDestroy() { }
    }
}