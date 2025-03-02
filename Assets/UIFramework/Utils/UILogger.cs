using UnityEngine;

namespace UIFramework.Utils
{
    /// <summary>
    /// UI框架日志工具类
    /// </summary>
    public static class UILogger
    {
        private const string LOG_PREFIX = "[UIFramework] ";
        private static bool _isEnabled = true;

        /// <summary>
        /// 是否启用日志
        /// </summary>
        public static bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        /// <summary>
        /// 输出普通日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public static void Log(string message)
        {
            if (!_isEnabled) return;
            Debug.Log($"{LOG_PREFIX}{message}");
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="message">警告消息</param>
        public static void LogWarning(string message)
        {
            if (!_isEnabled) return;
            Debug.LogWarning($"{LOG_PREFIX}{message}");
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="message">错误消息</param>
        public static void LogError(string message)
        {
            if (!_isEnabled) return;
            Debug.LogError($"{LOG_PREFIX}{message}");
        }
    }
}