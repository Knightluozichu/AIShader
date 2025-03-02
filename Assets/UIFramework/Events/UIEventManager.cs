using UnityEngine;

namespace UIFramework.Events
{
    /// <summary>
    /// UI事件管理器，提供全局事件分发功能
    /// </summary>
    public class UIEventManager : MonoBehaviour
    {
        private static UIEventManager _instance;
        public static UIEventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("UIEventManager");
                    _instance = go.AddComponent<UIEventManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private readonly EventDispatcher _eventDispatcher = new EventDispatcher();

        public void AddEventListener<T>(string eventType, System.Action<T> handler) where T : UIEventArgs
        {
            _eventDispatcher.AddEventListener(eventType, handler);
        }

        public void RemoveEventListener<T>(string eventType, System.Action<T> handler) where T : UIEventArgs
        {
            _eventDispatcher.RemoveEventListener(eventType, handler);
        }

        public void DispatchEvent<T>(string eventType, T eventArgs) where T : UIEventArgs
        {
            _eventDispatcher.DispatchEvent(eventType, eventArgs);
        }

        public bool HasEventListener(string eventType)
        {
            return _eventDispatcher.HasEventListener(eventType);
        }
    }
}