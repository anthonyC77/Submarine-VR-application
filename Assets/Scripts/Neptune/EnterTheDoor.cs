using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTheDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Names.PLAYER)
        {
            SceneManager.LoadScene("Fractal", LoadSceneMode.Single);
        }
    }
}
