using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static LevelManager GetInstance()
    {
        return instance;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void LoadStatsPage()
    {
        SceneManager.LoadScene("StatsPage");
    }

    public void LoadSettings()
    {
        GameObject instance = Instantiate(Resources.Load("Prefabs/SettingsPanel", typeof(GameObject))) as GameObject;
    }
    
    public void ShowSettings()
    {

    }
}
