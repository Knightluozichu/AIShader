using UnityEngine;
using UnityEditor;
using UIFramework.Theme;

public class ThemeCreator
{
#if UNITY_EDITOR
    [MenuItem("Tools/UI Framework/Create Default Theme")]
    public static void CreateDefaultTheme()
    {
        var theme = ScriptableObject.CreateInstance<UITheme>();
        theme.themeName = "Default Theme";
        theme.colors.primary = new Color(0.2f, 0.6f, 1f);
        theme.colors.secondary = new Color(0.1f, 0.3f, 0.5f);
        theme.colors.accent = new Color(1f, 0.5f, 0f);
        theme.colors.background = new Color(0.05f, 0.05f, 0.05f, 0.9f);
        theme.colors.text = Color.white;

        AssetDatabase.CreateAsset(theme, "Assets/Resources/Themes/DefaultTheme.asset");
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/UI Framework/Create Dark Theme")]
    public static void CreateDarkTheme()
    {
        var theme = ScriptableObject.CreateInstance<UITheme>();
        theme.themeName = "Dark Theme";
        theme.colors.primary = new Color(0.1f, 0.1f, 0.1f);
        theme.colors.secondary = new Color(0.2f, 0.2f, 0.2f);
        theme.colors.accent = new Color(0.5f, 0.5f, 0.5f);
        theme.colors.background = new Color(0.05f, 0.05f, 0.05f, 1f);
        theme.colors.text = new Color(0.8f, 0.8f, 0.8f);

        AssetDatabase.CreateAsset(theme, "Assets/Resources/Themes/DarkTheme.asset");
        AssetDatabase.SaveAssets();
    }
#endif
}