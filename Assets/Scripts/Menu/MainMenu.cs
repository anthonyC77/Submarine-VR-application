using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string Name;

    public void PlayGame()
    {
        var current = EventSystem.current;
        string nameButton = current.currentSelectedGameObject.name;

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        SceneManager.LoadScene(nameButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
