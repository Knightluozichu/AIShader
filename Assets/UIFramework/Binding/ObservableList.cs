using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UIFramework.Binding.Collection
{
    /// <summary>
    /// 可观察列表
    /// </summary>
    /// <typeparam name="T">列表项类型</typeparam>
    public class ObservableList<T> : Collection<T>
    {
        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;
        public event Action OnListCleared;

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnItemAdded?.Invoke(item);
        }

        protected override void RemoveItem(int index)
        {
            T item = this[index];
            base.RemoveItem(index);
            OnItemRemoved?.Invoke(item);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            OnListCleared?.Invoke();
        }
    }
}