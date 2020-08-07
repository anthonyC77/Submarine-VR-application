using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMailloche : OVRGrabbable
{
    private GameObject Recorder;
    bool start = true;

    public new void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        //Recorder.transform.position.y -= 0.1f;
        if (start)
        {
            Recorder.gameObject.transform.Translate(new Vector3(0, 0.1f, 0));
            start = false;
        }        
    }

    protected new void Start()
    {
        base.Start();
        Recorder = Helper.FindByTag(TagNames.RECORDER);
    }
}
