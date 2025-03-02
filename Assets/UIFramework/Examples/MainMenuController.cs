using UnityEngine;
using UIFramework.Events;
using UIFramework.Theme;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;
    
    // 添加公共属性
    public MainMenuView MainMenuView
    {
        get => _mainMenuView;
        set => _mainMenuView = value;
    }
    private MainMenuModel _model;
    private bool _isDarkTheme = false;

    private void Start()
    {
        InitializeModel();
        RegisterEventListeners();
    }

    private void InitializeModel()
    {
        _model = new MainMenuModel();
        
        // 设置初始值
        _model.PlayerName.Value = "Player_" + Random.Range(1000, 9999);
        _model.Coins.Value = 1000;
        _model.IsSoundEnabled.Value = true;
    }

    private void RegisterEventListeners()
    {
        UIEventManager.Instance.AddEventListener<UIEventArgs>(
            "MainMenu.PlayClicked", OnPlayClicked);
        UIEventManager.Instance.AddEventListener<UIEventArgs>(
            "MainMenu.SettingsClicked", OnSettingsClicked);
    }

    private void OnPlayClicked(UIEventArgs args)
    {
        // 保存当前状态
        PlayerPrefs.SetString("PlayerName", _model.PlayerName.Value);
        PlayerPrefs.SetInt("Coins", _model.Coins.Value);
        PlayerPrefs.SetInt("SoundEnabled", _model.IsSoundEnabled.Value ? 1 : 0);
        PlayerPrefs.Save();

        // 加载游戏场景
        GameSceneManager.Instance.LoadGameScene();
    }

    private void OnSettingsClicked(UIEventArgs args)
    {
        // 切换主题
        _isDarkTheme = !_isDarkTheme;
        ThemeManager.Instance.ApplyTheme(_isDarkTheme ? "Dark" : "Default");

        // 可以在这里打开设置面板或加载设置场景
        GameSceneManager.Instance.LoadSettingsScene();
    }

    private void OnDestroy()
    {
        // 移除事件监听
        UIEventManager.Instance.RemoveEventListener<UIEventArgs>(
            "MainMenu.PlayClicked", OnPlayClicked);
        UIEventManager.Instance.RemoveEventListener<UIEventArgs>(
            "MainMenu.SettingsClicked", OnSettingsClicked);
    }
}