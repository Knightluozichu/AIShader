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