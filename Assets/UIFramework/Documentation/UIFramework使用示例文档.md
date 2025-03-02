# UIFramework 使用教程

## 目录
1. 基础设置
2. 创建主题
3. 创建UI组件
4. 实现数据绑定
5. 使用事件系统
6. 示例：制作游戏主菜单

## 1. 基础设置

### 1.1 场景设置
1. 创建一个新的场景
2. 添加 Canvas 对象：GameObject -> UI -> Canvas
3. 确保场景中有 EventSystem：GameObject -> UI -> Event System
   

### 1.2 初始化管理器
创建一个初始化脚本：

```csharp name=Assets/Scripts/UI/UIInitializer.cs
using UnityEngine;
using UIFramework.Theme;
using UIFramework.Events;
using UIFramework.Rendering.BatchingSystem;

public class UIInitializer : MonoBehaviour
{
 void Awake()
 {
     // 确保所有管理器都被初始化
     var themeManager = ThemeManager.Instance;
     var eventManager = UIEventManager.Instance;
     var batchingManager = UIBatchingManager.Instance;
 }
}
```

2. 创建主题
2.1 创建主题配置
```csharp
using UnityEngine;
using UIFramework.Theme;

[CreateAssetMenu(fileName = "GameUITheme", menuName = "Game/UI/Theme")]
public class GameUITheme : UITheme
{
    private void OnEnable()
    {
        // 设置默认主题颜色
        colors.primary = new Color(0.2f, 0.6f, 1f);
        colors.secondary = new Color(0.1f, 0.3f, 0.5f);
        colors.background = new Color(0.05f, 0.05f, 0.05f, 0.9f);
        colors.text = Color.white;
    }
}
```

2.2 注册主题

```csharp
using UnityEngine;
using UIFramework.Theme;

public class ThemeSetup : MonoBehaviour
{
    [SerializeField] private GameUITheme defaultTheme;
    [SerializeField] private GameUITheme darkTheme;

    void Start()
    {
        // 注册主题
        ThemeManager.Instance.RegisterTheme("Default", defaultTheme);
        ThemeManager.Instance.RegisterTheme("Dark", darkTheme);

        // 应用默认主题
        ThemeManager.Instance.ApplyTheme("Default");
    }
}
```

3. 示例：游戏主菜单实现
3.1 创建数据模型
```csharp
using UIFramework.Binding;

public class MainMenuModel
{
    public BindableProperty<string> PlayerName { get; } = new BindableProperty<string>("Player");
    public BindableProperty<int> Coins { get; } = new BindableProperty<int>(0);
    public BindableProperty<bool> IsSoundEnabled { get; } = new BindableProperty<bool>(true);
}
```

3.2 创建主菜单视图
```csharp
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
```


3.3 创建主菜单预制体
- 在场景中创建 UI 层级结构：
  - MainMenuCanvas (Canvas)
    └── MainMenuPanel (Panel)
        ├── TitleText (Text)
        ├── PlayerNameText (Text)
        ├── CoinsText (Text)
        ├── ButtonsPanel (Panel)
        │   ├── PlayButton (Button)
        │   └── SettingsButton (Button)
        └── OptionsPanel (Panel)
            └── SoundToggle (Toggle)
- 将 MainMenuView 脚本添加到 MainMenuPanel

3.4 创建主菜单控制器

```csharp
using UnityEngine;
using UIFramework.Events;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuView mainMenuView;

    private void Start()
    {
        // 注册事件监听
        UIEventManager.Instance.AddEventListener<UIEventArgs>(
            "MainMenu.PlayClicked", OnPlayClicked);
        UIEventManager.Instance.AddEventListener<UIEventArgs>(
            "MainMenu.SettingsClicked", OnSettingsClicked);
    }

    private void OnPlayClicked(UIEventArgs args)
    {
        Debug.Log("Starting game...");
        // 实现游戏开始逻辑
    }

    private void OnSettingsClicked(UIEventArgs args)
    {
        Debug.Log("Opening settings...");
        // 实现打开设置面板逻辑
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
```

4. 使用特效和渲染
```csharp
using UnityEngine;
using UIFramework.Rendering.Effects;

public class MenuBackgroundEffect : UIGradientEffect
{
    protected override void Awake()
    {
        base.Awake();
        
        // 设置渐变颜色
        SetGradientColors(
            new Color(0.1f, 0.2f, 0.4f, 1f),
            new Color(0.2f, 0.4f, 0.8f, 1f)
        );
        
        // 设置渐变方向
        SetGradientDirection(Vector2.up);
    }
}
```

4.2 添加模糊效果
```csharp
using UnityEngine;
using UIFramework.Rendering.Effects;

public class MenuBlurEffect : UIBlurEffect
{
    protected override void Awake()
    {
        base.Awake();
        SetBlurParameters(2.0f, 3);
    }
}
```

5. 完整流程示例
创建场景并设置基础UI结构
Scene
├── UIInitializer (GameObject + UIInitializer.cs)
├── ThemeSetup (GameObject + ThemeSetup.cs)
├── MainCanvas (Canvas)
│   └── MainMenuPanel (Panel + MainMenuView.cs)
└── EventSystem

创建主题资源：

创建 GameUITheme ScriptableObject
设置主题颜色和样式
在 Unity 编辑器中：

将主题资源分配给 ThemeSetup 组件
设置 MainMenuView 中的所有引用
添加特效组件到相应的面板
运行游戏，检查主菜单是否正常工作：

主题应用正确
数据绑定响应更新
按钮点击事件正常触发
特效正确显示
常见问题解决
如果UI不显示：

检查 Canvas 设置是否正确
确认 RectTransform 的位置和大小
验证材质和贴图是否正确分配
如果事件不响应：

确保 EventSystem 存在
检查事件监听器是否正确注册
验证按钮等交互组件的 RaycastTarget 是否启用
如果主题不生效：

确认主题已正确注册
检查 IThemeable 接口实现
验证主题资源的设置
最佳实践
组织结构：

将UI预制体放在 Resources/Prefabs/UI 目录下
将主题资源放在 Resources/Themes 目录下
将脚本按功能分类放在相应目录中
命名规范：

视图类以 View 结尾
控制器类以 Controller 结尾
模型类以 Model 结尾
性能优化：

使用对象池管理频繁创建的UI元素
合理使用批处理系统
避免在Update中频繁更新UI


这个教程提供了：
1. 完整的设置流程
2. 具体的示例代码
3. 最佳实践建议
4. 常见问题解决方案

你可以按照这个教程一步一步实现一个完整的UI界面。需要更具体的某个部分的说明吗？