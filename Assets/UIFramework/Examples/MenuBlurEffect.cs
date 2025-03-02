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