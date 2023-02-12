using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Fog : MonoBehaviour
{
    public Camera camera;
    public GameObject ActionsText;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogColor = camera.backgroundColor;
        RenderSettings.fogDensity = 0.03f;
        RenderSettings.fog = true;

        XRSettings.eyeTextureResolutionScale = 0.5f;
    }

    bool showActions = false;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown("a"))
        {
            showActions = true;
        }

        if (OVRInput.GetUp(OVRInput.Button.Two) || Input.GetKeyUp("a"))
        {
            showActions = false;
        }

        
        ActionsText.SetActive(showActions);
    }
}
