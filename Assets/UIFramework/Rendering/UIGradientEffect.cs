using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Rendering.Effects
{
    /// <summary>
    /// UI渐变效果
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class UIGradientEffect : UIRendererBase
    {
        [SerializeField] private Color _startColor = Color.white;
        [SerializeField] private Color _endColor = Color.black;
        [SerializeField] private Vector2 _gradientDirection = Vector2.up;

        private Material _gradientMaterial;

        protected override void Awake()
        {
            base.Awake();
            InitializeGradientMaterial();
        }

        private void InitializeGradientMaterial()
        {
            if (_gradientMaterial == null)
            {
                // 这里假设你有一个专门的渐变shader
                _gradientMaterial = new Material(Shader.Find("UI/Gradient"));
                if (_gradientMaterial != null)
                {
                    UpdateMaterialProperties();
                }
            }
        }

        public override void UpdateRenderData()
        {
            if (_canvasRenderer == null || _gradientMaterial == null)
                return;

            UpdateMaterialProperties();
            _canvasRenderer.SetMaterial(_gradientMaterial, null);
        }

        private void UpdateMaterialProperties()
        {
            if (_gradientMaterial != null)
            {
                _gradientMaterial.SetColor("_StartColor", _startColor);
                _gradientMaterial.SetColor("_EndColor", _endColor);
                _gradientMaterial.SetVector("_GradientDirection", _gradientDirection.normalized);
            }
        }

        public void SetGradientColors(Color startColor, Color endColor)
        {
            _startColor = startColor;
            _endColor = endColor;
            _isDirty = true;
        }

        public void SetGradientDirection(Vector2 direction)
        {
            _gradientDirection = direction.normalized;
            _isDirty = true;
        }

        protected override void OnDestroy()
        {
            if (_gradientMaterial != null)
            {
                Destroy(_gradientMaterial);
            }
        }
    }
}