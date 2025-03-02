using System;
using System.Collections.Generic;
using UIFramework.Utils;

namespace UIFramework.Events
{
    /// <summary>
    /// 事件分发器实现
    /// </summary>
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<string, List<Delegate>> _eventHandlers = new Dictionary<string, List<Delegate>>();

        public void AddEventListener<T>(string eventType, Action<T> handler) where T : UIEventArgs
        {
            if (string.IsNullOrEmpty(eventType))
            {
                UILogger.LogError("Event type cannot be null or empty");
                return;
            }

            if (handler == null)
            {
                UILogger.LogError("Event handler cannot be null");
                return;
            }

            if (!_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] = new List<Delegate>();
            }

            if (!_eventHandlers[eventType].Contains(handler))
            {
                _eventHandlers[eventType].Add(handler);
                UILogger.Log($"Added event listener for event type: {eventType}");
            }
        }

        public void RemoveEventListener<T>(string eventType, Action<T> handler) where T : UIEventArgs
        {
            if (string.IsNullOrEmpty(eventType))
            {
                UILogger.LogError("Event type cannot be null or empty");
                return;
            }

            if (handler == null)
            {
                UILogger.LogError("Event handler cannot be null");
                return;
            }

            if (_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType].Remove(handler);
                if (_eventHandlers[eventType].Count == 0)
                {
                    _eventHandlers.Remove(eventType);
                }
                UILogger.Log($"Removed event listener for event type: {eventType}");
            }
        }

        public void DispatchEvent<T>(string eventType, T eventArgs) where T : UIEventArgs
        {
            if (string.IsNullOrEmpty(eventType))
            {
                UILogger.LogError("Event type cannot be null or empty");
                return;
            }

            if (eventArgs == null)
            {
                UILogger.LogError("Event args cannot be null");
                return;
            }

            if (_eventHandlers.TryGetValue(eventType, out var handlers))
            {
                // 创建副本以防在处理过程中列表被修改
                var handlersCopy = new List<Delegate>(handlers);
                foreach (var handler in handlersCopy)
                {
                    try
                    {
                        if (handler is Action<T> typedHandler)
                        {
                            typedHandler(eventArgs);
                        }
                    }
                    catch (Exception ex)
                    {
                        UILogger.LogError($"Error dispatching event {eventType}: {ex.Message}");
                    }
                }
            }
        }

        public bool HasEventListener(string eventType)
        {
            return !string.IsNullOrEmpty(eventType) && _eventHandlers.ContainsKey(eventType);
        }
    }
}