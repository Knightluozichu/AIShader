using UnityEngine;
using UnityEngine.UI;
using UIFramework.Core;
using UIFramework.Theme;
using UIFramework.Events;
using UIFramework.Binding;

public class MainMenuView : UIComponentBase, IThemeable
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text playerNameText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Toggle soundToggle;

    private MainMenuModel _model;
    private DataBindingContext _bindingContext;

    protected override void Awake()
    {
        base.Awake();
        _bindingContext = new DataBindingContext();
        _model = new MainMenuModel();
        InitializeBindings();
        SetupEventListeners();
    }

    private void InitializeBindings()
    {
        // 绑定玩家名称
        _model.PlayerName.OnValueChanged += value => playerNameText.text = $"Welcome, {value}!";

        // 绑定金币数量
        _model.Coins.OnValueChanged += value => coinsText.text = $"Coins: {value}";

        // 绑定声音设置
        _model.IsSoundEnabled.OnValueChanged += value => soundToggle.isOn = value;
    }

    private void SetupEventListeners()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
    }

    private void OnPlayClicked()
    {
        UIEventManager.Instance.DispatchEvent("MainMenu.PlayClicked", 
            new UIEventArgs(this));
    }

    private void OnSettingsClicked()
    {
        UIEventManager.Instance.DispatchEvent("MainMenu.SettingsClicked", 
            new UIEventArgs(this));
    }

    private void OnSoundToggleChanged(bool isOn)
    {
        _model.IsSoundEnabled.Value = isOn;
    }

    public void ApplyTheme(UITheme theme)
    {
        titleText.color = theme.colors.primary;
        playerNameText.color = theme.colors.text;
        coinsText.color = theme.colors.secondary;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        var currentTheme = ThemeManager.Instance.GetCurrentTheme();
        if (currentTheme != null)
        {
            ApplyTheme(currentTheme);
        }
    }
}