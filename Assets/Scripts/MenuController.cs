using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public string gameSceneName;

    public void CloseGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }

}
