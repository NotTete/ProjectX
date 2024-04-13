using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMainMenu : MonoBehaviour
{
    [SerializeField] string scene;

    public void exitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene(scene);
    }
}
