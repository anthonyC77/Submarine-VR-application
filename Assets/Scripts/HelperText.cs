using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperText : MonoBehaviour
{
    public GameObject ActionsText;

    bool showActions = false;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown("a"))
        {
            showActions = true;
        }

        if (OVRInput.GetUp(OVRInput.Button.One) || Input.GetKeyUp("a"))
        {
            showActions = false;
        }


        ActionsText.SetActive(showActions);
    }
}
