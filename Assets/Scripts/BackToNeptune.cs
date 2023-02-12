using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToNeptune : MonoBehaviour
{
    string scene = Names.EMDR;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(time());
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(250);        
        SceneManager.LoadScene(scene);
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
            SceneManager.LoadScene(scene);

    }
}
