using UnityEngine;
using UnityEngine.UI;
using UIFramework.Theme;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuSceneSetup : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/UI Framework/Setup Main Menu Scene")]
    public static void SetupMainMenuScene()
    {
        // 创建基础结构
        var gameBootstrapGO = new GameObject("GameBootstrap");
        var mainMenuControllerGO = new GameObject("MainMenuController");
        var managersGO = new GameObject("Managers");

        // 添加组件
        var gameBootstrap = gameBootstrapGO.AddComponent<GameBootstrap>();
        var mainMenuController = mainMenuControllerGO.AddComponent<MainMenuController>();

        // 添加管理器组件
        var uiInitializer = managersGO.AddComponent<UIInitializer>();

        // 确保有Canvas
        Canvas canvas = Object.FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            var canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        // 加载并实例化主菜单预制体
        var mainMenuPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefabs/UI/MainMenu.prefab");
        if (mainMenuPrefab != null)
        {
            var mainMenuInstance = PrefabUtility.InstantiatePrefab(mainMenuPrefab, canvas.transform) as GameObject;
            var mainMenuView = mainMenuInstance.GetComponent<MainMenuView>();

            // 使用公共属性设置引用
            mainMenuController.MainMenuView = mainMenuView;
        }
        else
        {
            Debug.LogError("MainMenu prefab not found at Assets/Resources/Prefabs/UI/MainMenu.prefab");
        }

        // 加载主题资源
        var defaultTheme = AssetDatabase.LoadAssetAtPath<UITheme>("Assets/Resources/Themes/DefaultTheme.asset");
        var darkTheme = AssetDatabase.LoadAssetAtPath<UITheme>("Assets/Resources/Themes/DarkTheme.asset");

        if (defaultTheme == null || darkTheme == null)
        {
            Debug.LogError("Theme assets not found. Please create themes first using ThemeCreator.");
        }

        // 使用公共属性设置主题引用
        gameBootstrap.DefaultTheme = defaultTheme;
        gameBootstrap.DarkTheme = darkTheme;
        gameBootstrap.MainMenuController = mainMenuController;

        // 确保有EventSystem
        if (Object.FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            var eventSystemGO = new GameObject("EventSystem");
            eventSystemGO.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystemGO.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        // 设置场景层级
        gameBootstrapGO.transform.SetSiblingIndex(0);
        managersGO.transform.SetSiblingIndex(1);
        mainMenuControllerGO.transform.SetSiblingIndex(2);

        // 选择GameBootstrap对象以便查看Inspector
        Selection.activeGameObject = gameBootstrapGO;

        Debug.Log("Main Menu Scene setup completed!");
    }
#endif
}