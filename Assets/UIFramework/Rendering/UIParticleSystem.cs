using UIFramework.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Rendering
{
    /// <summary>
    /// UI粒子系统
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer), typeof(ParticleSystem))]
    public class UIParticleSystem : UIRendererBase
    {
        private ParticleSystem _particleSystem;
        private ParticleSystemRenderer _particleRenderer;
        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];

        protected override void Awake()
        {
            base.Awake();
            _particleSystem = GetComponent<ParticleSystem>();
            _particleRenderer = GetComponent<ParticleSystemRenderer>();

            if (_particleSystem == null || _particleRenderer == null)
            {
                UILogger.LogError("ParticleSystem components are required");
                return;
            }

            // 确保粒子系统在Canvas空间中正确渲染
            var main = _particleSystem.main;
            main.scalingMode = ParticleSystemScalingMode.Local;
            main.playOnAwake = false;

            // 设置默认材质
            if (_material == null)
            {
                _material = new Material(Shader.Find("UI/Particles/Additive"));
            }
        }

        public override void UpdateRenderData()
        {
            if (_particleSystem == null || _canvasRenderer == null)
                return;

            // 更新粒子状态
            int particleCount = _particleSystem.GetParticles(_particles);
            
            // 更新Canvas渲染器
            if (_canvasRenderer != null && _material != null)
            {
                _canvasRenderer.SetMaterial(_material, null);
                // 这里可以根据需要设置其他渲染属性
            }
        }

        public void Play()
        {
            if (_particleSystem != null && _isEnabled)
            {
                _particleSystem.Play();
            }
        }

        public void Stop()
        {
            if (_particleSystem != null)
            {
                _particleSystem.Stop();
            }
        }

        public void Pause()
        {
            if (_particleSystem != null)
            {
                _particleSystem.Pause();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_particleSystem != null && _particleSystem.main.playOnAwake)
            {
                _particleSystem.Play();
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (_particleSystem != null)
            {
                _particleSystem.Stop();
            }
        }
    }
}