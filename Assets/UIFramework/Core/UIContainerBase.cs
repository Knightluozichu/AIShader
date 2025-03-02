using System.Collections.Generic;
using UnityEngine;

namespace UIFramework.Core
{
    /// <summary>
    /// UI容器基类，实现IUIContainer接口的基本功能
    /// </summary>
    public abstract class UIContainerBase : UIComponentBase, IUIContainer
    {
        private readonly List<IUIComponent> _children = new List<IUIComponent>();

        public void AddChild(IUIComponent child)
        {
            if (child == null || _children.Contains(child)) return;

            _children.Add(child);
            if (child is MonoBehaviour childMono)
            {
                childMono.transform.SetParent(transform, false);
            }
            OnChildAdded(child);
        }

        public void RemoveChild(IUIComponent child)
        {
            if (child == null || !_children.Contains(child)) return;

            OnChildRemoved(child);
            _children.Remove(child);
            if (child is MonoBehaviour childMono)
            {
                childMono.transform.SetParent(null);
            }
        }

        public IUIComponent GetChild(int index)
        {
            return index >= 0 && index < _children.Count ? _children[index] : null;
        }

        public IEnumerable<IUIComponent> GetChildren()
        {
            return _children.AsReadOnly();
        }

        public override void Destroy()
        {
            foreach (var child in _children.ToArray())
            {
                child.Destroy();
            }
            _children.Clear();
            base.Destroy();
        }

        protected virtual void OnChildAdded(IUIComponent child) { }
        protected virtual void OnChildRemoved(IUIComponent child) { }
    }
}