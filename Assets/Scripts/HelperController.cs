using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperController : MonoBehaviour
{
    public GameObject ImageController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(time());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(10);
        ImageController.SetActive(false);
    }
}
