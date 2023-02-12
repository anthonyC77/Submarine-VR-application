using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnterPlanetScene : MonoBehaviour
{
    public GameObject PlayerMain;
    public Animator Transition;
    public float WaitTime = 5.0f;
    public GameObject ChangeLevel;


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision on " + collision.name);
        if (collision.gameObject.tag == Names.PLAYER)
        {
            //var scene = ePlanet.Neptune.ToString();
            //if (this.tag == scene)
            //{
            //    Debug.Log("Enter scene " + scene);
            //    //SceneManager.LoadScene(scene, LoadSceneMode.Single);
            //    StartCoroutine(LoadYourAsyncScene(scene));
            //    PlayerMain.SetActive(false);
            //}
            LoadNextLevel();
        }
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        ChangeLevel.SetActive(true);

        yield return new WaitForSeconds(WaitTime);

        var scene = ePlanet.Neptune.ToString();
        // load scene
        SceneManager.LoadScene(scene);
    }
}
