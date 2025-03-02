namespace UIFramework.Events.Common
{
    /// <summary>
    /// UI点击事件参数
    /// </summary>
    public class UIClickEventArgs : UIEventArgs
    {
        /// <summary>
        /// 点击位置
        /// </summary>
        public UnityEngine.Vector2 Position { get; private set; }

        /// <summary>
        /// 点击的UI元素ID
        /// </summary>
        public string ElementId { get; private set; }

        public UIClickEventArgs(object source, UnityEngine.Vector2 position, string elementId) 
            : base(source)
        {
            Position = position;
            ElementId = elementId;
        }
    }
}