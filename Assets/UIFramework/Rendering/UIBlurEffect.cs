using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Rendering.Effects
{
    /// <summary>
    /// UI模糊效果
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class UIBlurEffect : UIRendererBase
    {
        [SerializeField] private float _blurSize = 2.0f;
        [SerializeField] private int _iterations = 3;

        private RenderTexture _sourceTexture;
        private RenderTexture _tempTexture;
        private Material _blurMaterial;

        protected override void Awake()
        {
            base.Awake();
            InitializeBlurMaterial();
        }

        private void InitializeBlurMaterial()
        {
            if (_blurMaterial == null)
            {
                // 这里假设你有一个专门的模糊shader
                _blurMaterial = new Material(Shader.Find("UI/Blur"));
                if (_blurMaterial != null)
                {
                    _blurMaterial.SetFloat("_BlurSize", _blurSize);
                }
            }
        }

        public override void UpdateRenderData()
        {
            if (_canvasRenderer == null || _blurMaterial == null)
                return;

            // 更新模糊材质参数
            _blurMaterial.SetFloat("_BlurSize", _blurSize);
            _canvasRenderer.SetMaterial(_blurMaterial, null);

            // 在这里可以添加更多的渲染数据更新逻辑
        }

        public void SetBlurParameters(float blurSize, int iterations)
        {
            _blurSize = Mathf.Max(0, blurSize);
            _iterations = Mathf.Clamp(iterations, 1, 10);
            _isDirty = true;
        }

        protected override void OnDestroy()
        {
            if (_sourceTexture != null)
            {
                _sourceTexture.Release();
                Destroy(_sourceTexture);
            }

            if (_tempTexture != null)
            {
                _tempTexture.Release();
                Destroy(_tempTexture);
            }

            if (_blurMaterial != null)
            {
                Destroy(_blurMaterial);
            }
        }
    }
}