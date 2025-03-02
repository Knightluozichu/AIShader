using UnityEngine;
using UIFramework.Events;
using UIFramework.Theme;
using UIFramework.Rendering.BatchingSystem;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private UITheme _defaultTheme;
    [SerializeField] private UITheme _darkTheme;

    // 添加公共属性
    public MainMenuController MainMenuController
    {
        get => _mainMenuController;
        set => _mainMenuController = value;
    }

    public UITheme DefaultTheme
    {
        get => _defaultTheme;
        set => _defaultTheme = value;
    }

    public UITheme DarkTheme
    {
        get => _darkTheme;
        set => _darkTheme = value;
    }

    private void Awake()
    {
        var eventManager = UIEventManager.Instance;
        var themeManager = ThemeManager.Instance;
        var batchingManager = UIBatchingManager.Instance;
        var sceneManager = GameSceneManager.Instance;

        themeManager.RegisterTheme("Default", _defaultTheme);
        themeManager.RegisterTheme("Dark", _darkTheme);
        themeManager.ApplyTheme("Default");
    }

    private void Start()
    {
        if (_mainMenuController != null)
        {
            _mainMenuController.gameObject.SetActive(true);
        }
    }
}