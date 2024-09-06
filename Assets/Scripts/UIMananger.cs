using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMananger : MonoBehaviour
{
    public void ButtonHandlerStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonHandlerQuit()
    {
        Application.Quit();
    }
}
