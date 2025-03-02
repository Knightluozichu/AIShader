using System;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.Core;

namespace UIFramework.Utils
{
    /// <summary>
    /// UI对象池，用于管理和复用UI组件
    /// </summary>
    public class UIObjectPool : MonoBehaviour
    {
        private static UIObjectPool _instance;
        public static UIObjectPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("UIObjectPool");
                    _instance = go.AddComponent<UIObjectPool>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private readonly Dictionary<string, Queue<IUIComponent>> _pools = new Dictionary<string, Queue<IUIComponent>>();
        private readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

        /// <summary>
        /// 从对象池获取UI组件
        /// </summary>
        /// <typeparam name="T">UI组件类型</typeparam>
        /// <param name="prefab">预制体</param>
        /// <returns>UI组件实例</returns>
        public T Get<T>(GameObject prefab) where T : Component, IUIComponent
        {
            string key = prefab.name;

            if (!_prefabs.ContainsKey(key))
            {
                _prefabs[key] = prefab;
            }

            if (!_pools.TryGetValue(key, out Queue<IUIComponent> pool))
            {
                pool = new Queue<IUIComponent>();
                _pools[key] = pool;
            }

            IUIComponent component;
            if (pool.Count > 0)
            {
                component = pool.Dequeue();
                component.RectTransform.gameObject.SetActive(true);
            }
            else
            {
                var go = Instantiate(prefab, transform);
                component = go.GetComponent<T>();
                if (component == null)
                {
                    component = go.AddComponent<T>();
                }
            }

            return (T)component;
        }

        /// <summary>
        /// 将UI组件返回到对象池
        /// </summary>
        /// <param name="component">要返回的UI组件</param>
        public void Release(IUIComponent component)
        {
            if (component == null) return;

            var go = (component as MonoBehaviour)?.gameObject;
            if (go == null) return;

            string key = go.name.Replace("(Clone)", "");

            if (!_pools.TryGetValue(key, out Queue<IUIComponent> pool))
            {
                pool = new Queue<IUIComponent>();
                _pools[key] = pool;
            }

            go.SetActive(false);
            go.transform.SetParent(transform);
            pool.Enqueue(component);
        }

        /// <summary>
        /// 清空指定类型的对象池
        /// </summary>
        /// <param name="prefabName">预制体名称</param>
        public void ClearPool(string prefabName)
        {
            if (_pools.TryGetValue(prefabName, out Queue<IUIComponent> pool))
            {
                while (pool.Count > 0)
                {
                    var component = pool.Dequeue();
                    if (component is MonoBehaviour mono)
                    {
                        Destroy(mono.gameObject);
                    }
                }
                _pools.Remove(prefabName);
                _prefabs.Remove(prefabName);
            }
        }

        /// <summary>
        /// 清空所有对象池
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in _pools.Values)
            {
                while (pool.Count > 0)
                {
                    var component = pool.Dequeue();
                    if (component is MonoBehaviour mono)
                    {
                        Destroy(mono.gameObject);
                    }
                }
            }
            _pools.Clear();
            _prefabs.Clear();
        }
    }
}