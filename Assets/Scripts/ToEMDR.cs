using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToEMDR : MonoBehaviour
{
    IEnumerator time()
    {
        yield return new WaitForSeconds(1);
        var scene = Names.EMDR;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Names.PLAYER)
        {
            StartCoroutine(time());
        }
    }
}
