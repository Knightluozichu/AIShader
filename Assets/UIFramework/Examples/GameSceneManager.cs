using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager _instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("GameSceneManager");
                _instance = go.AddComponent<GameSceneManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}