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