using UnityEngine.UI;
using UIFramework.Utils;

namespace UIFramework.Binding.Adapters
{
    /// <summary>
    /// Text组件的数据绑定适配器
    /// </summary>
    public class TextBindingAdapter : BindableComponent
    {
        private Text _text;
        private BindableProperty<string> _textProperty;

        protected override void InitializeBindings()
        {
            _text = GetComponent<Text>();
            if (_text == null)
            {
                UILogger.LogError("TextBindingAdapter requires a Text component");
                return;
            }

            _textProperty = BindingContext.RegisterProperty<string>("Text", _text.text);
            _textProperty.OnValueChanged += OnTextChanged;
        }

        private void OnTextChanged(string newValue)
        {
            if (_text != null)
            {
                _text.text = newValue;
            }
        }

        protected override void OnDestroy()
        {
            if (_textProperty != null)
            {
                _textProperty.OnValueChanged -= OnTextChanged;
            }
            base.OnDestroy();
        }
    }
}