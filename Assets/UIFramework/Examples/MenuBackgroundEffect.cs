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