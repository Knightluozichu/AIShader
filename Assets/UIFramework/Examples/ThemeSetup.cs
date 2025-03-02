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