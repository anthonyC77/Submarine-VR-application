using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaillocheKikongiTwoFaces : MonoBehaviour
{
    List<GameObject> Notes = new List<GameObject>();
    Recorder recorder;

    // Start is called before the first frame update
    void Start()
    {
        Notes = GameObject.FindGameObjectsWithTag(Names.NOTES).ToList();
        recorder = new Recorder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
