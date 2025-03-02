# UIFramework 架构设计文档

## 1. 编码规范与命名约定

### 1.1 命名规范

#### 命名空间
```csharp
namespace UIFramework.Core
namespace UIFramework.Layout
namespace UIFramework.Theme
namespace UIFramework.Events
namespace UIFramework.Binding
namespace UIFramework.Rendering
namespace UIFramework.Utils
```

#### 类命名
- 使用 PascalCase
- 接口以 "I" 开头
- 抽象类以 "Base" 结尾
- 特定功能的组件以其功能命名

例如：
```csharp
public interface IUIComponent
public abstract class UIComponentBase
public class GridLayout
public class UIThemeManager
```

#### 方法命名
- 使用 PascalCase
- 动词开头
- 清晰表达功能意图

例如：
```csharp
public void Initialize()
public void UpdateLayout()
public void RegisterCallback()
```

#### 属性和字段
- 公共属性使用 PascalCase
- 私有字段使用 camelCase 并以下划线开头
- 常量使用全大写加下划线

例如：
```csharp
public Vector2 Position { get; set; }
private float _currentAlpha;
private const float MAX_SCALE = 1.5f;
```

### 1.2 代码组织

#### 文件组织
- 每个类一个文件
- 文件名与类名完全一致
- 按功能模块组织目录结构

#### 代码结构
```
UIFramework/
├── Core/           # 核心接口和基类
├── Layout/         # 布局系统
├── Theme/          # 主题系统
├── Events/         # 事件系统
├── Binding/        # 数据绑定
├── Rendering/      # 渲染相关
└── Utils/          # 工具类
```

#### 类内部组织顺序
1. 常量和静态字段
2. 实例字段
3. 属性
4. 构造函数
5. Unity生命周期方法
6. 公共方法
7. 保护方法
8. 私有方法

## 2. 模块化设计

### 2.1 核心模块 (Core)

#### IUIComponent
基础UI组件接口，定义所有UI组件必须实现的基本功能。

```csharp
public interface IUIComponent
{
    RectTransform RectTransform { get; }
    bool IsVisible { get; set; }
    void Initialize();
    void Show();
    void Hide();
    void Destroy();
}
```

#### IUIContainer
容器接口，用于管理子组件。

```csharp
public interface IUIContainer : IUIComponent
{
    void AddChild(IUIComponent child);
    void RemoveChild(IUIComponent child);
    IUIComponent GetChild(int index);
    IEnumerable<IUIComponent> GetChildren();
}
```

### 2.2 布局模块 (Layout)

#### ILayoutStrategy
布局策略接口。

```csharp
public interface ILayoutStrategy
{
    void CalculateLayout(IUIContainer container);
    void ApplyLayout(IUIContainer container);
}
```

主要实现类：
- GridLayoutStrategy
- FlexLayoutStrategy
- AnchorLayoutStrategy

### 2.3 主题模块 (Theme)

#### IThemeManager
主题管理接口。

```csharp
public interface IThemeManager
{
    void RegisterTheme(string themeName, UITheme theme);
    void ApplyTheme(string themeName);
    void UpdateTheme(string themeName, Action<UITheme> updateAction);
}
```

### 2.4 事件模块 (Events)

#### IEventDispatcher
事件分发接口。

```csharp
public interface IEventDispatcher
{
    void AddEventListener<T>(string eventType, Action<T> handler);
    void RemoveEventListener<T>(string eventType, Action<T> handler);
    void DispatchEvent<T>(string eventType, T eventData);
}
```

### 2.5 数据绑定模块 (Binding)

#### IBindable
可绑定接口。

```csharp
public interface IBindable<T>
{
    T Value { get; set; }
    event Action<T> OnValueChanged;
}
```

### 2.6 渲染模块 (Rendering)

#### IUIRenderer
渲染接口。

```csharp
public interface IUIRenderer
{
    void SetMaterial(Material material);
    void UpdateRenderData();
    void Render();
}
```

## 3. 性能优化策略

### 3.1 对象池
- 实现UI组件对象池
- 复用频繁创建销毁的UI元素

### 3.2 渲染优化
- Draw Call 批处理
- 自动图集管理
- UI合批策略

### 3.3 内存管理
- 异步资源加载
- 资源引用计数
- 自动内存释放

## 4. 使用示例

### 4.1 创建简单UI面板

```csharp
public class SimplePanel : UIComponentBase
{
    public SimplePanel()
    {
        // 初始化面板
    }

    public override void Initialize()
    {
        // 初始化逻辑
    }

    protected override void OnShow()
    {
        // 显示逻辑
    }

    protected override void OnHide()
    {
        // 隐藏逻辑
    }
}
```

### 4.2 使用数据绑定

```csharp
public class InventoryItem : UIComponentBase
{
    private IBindable<int> _count;
    
    public void BindCount(IBindable<int> count)
    {
        _count = count;
        _count.OnValueChanged += UpdateCountDisplay;
    }
    
    private void UpdateCountDisplay(int newCount)
    {
        // 更新显示
    }
}
```

## 5. 版本规划

### 5.1 第一阶段 (v0.1.0)
- 核心接口设计
- 基础布局系统
- 简单事件系统

### 5.2 第二阶段 (v0.2.0)
- 主题系统
- 数据绑定
- 对象池优化

### 5.3 第三阶段 (v0.3.0)
- 高级布局功能
- 渲染优化
- 完整的示例和文档