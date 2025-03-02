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