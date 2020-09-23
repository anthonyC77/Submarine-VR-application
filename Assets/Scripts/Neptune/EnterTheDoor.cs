using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTheDoor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Names.PLAYER)
        {
            SceneManager.LoadScene("FractalJourney", LoadSceneMode.Single);
        }
    }
}
