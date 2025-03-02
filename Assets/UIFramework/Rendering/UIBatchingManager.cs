using System.Collections.Generic;
using UnityEngine;
using UIFramework.Utils;

namespace UIFramework.Rendering.BatchingSystem
{
    /// <summary>
    /// UI批处理管理器
    /// </summary>
    public class UIBatchingManager : MonoBehaviour
    {
        private static UIBatchingManager _instance;
        public static UIBatchingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("UIBatchingManager");
                    _instance = go.AddComponent<UIBatchingManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private Dictionary<Material, List<IUIRenderer>> _batchGroups = new Dictionary<Material, List<IUIRenderer>>();
        private bool _isDirty = false;

        /// <summary>
        /// 注册UI渲染器到批处理系统
        /// </summary>
        public void RegisterRenderer(IUIRenderer renderer)
        {
            if (renderer == null || renderer.GetMaterial() == null) return;

            Material material = renderer.GetMaterial();
            if (!_batchGroups.ContainsKey(material))
            {
                _batchGroups[material] = new List<IUIRenderer>();
            }

            if (!_batchGroups[material].Contains(renderer))
            {
                _batchGroups[material].Add(renderer);
                _isDirty = true;
            }
        }

        /// <summary>
        /// 从批处理系统移除UI渲染器
        /// </summary>
        public void UnregisterRenderer(IUIRenderer renderer)
        {
            if (renderer == null || renderer.GetMaterial() == null) return;

            Material material = renderer.GetMaterial();
            if (_batchGroups.TryGetValue(material, out var group))
            {
                if (group.Remove(renderer))
                {
                    _isDirty = true;
                }

                if (group.Count == 0)
                {
                    _batchGroups.Remove(material);
                }
            }
        }

        /// <summary>
        /// 更新批处理组
        /// </summary>
        private void UpdateBatches()
        {
            if (!_isDirty) return;

            foreach (var group in _batchGroups.Values)
            {
                // 按照渲染顺序排序
                group.Sort((a, b) => a.SortingOrder.CompareTo(b.SortingOrder));
            }

            _isDirty = false;
        }

        private void LateUpdate()
        {
            UpdateBatches();

            // 执行批处理渲染
            foreach (var pair in _batchGroups)
            {
                Material material = pair.Key;
                List<IUIRenderer> renderers = pair.Value;

                foreach (var renderer in renderers)
                {
                    if (renderer.IsEnabled)
                    {
                        renderer.Render();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _batchGroups.Clear();
            _instance = null;
        }
    }
}