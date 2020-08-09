using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHeadPhone : OVRGrabbable
{
    public new void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        var name = this.name;
        int nb = int.Parse(name.Replace(TagNames.HEADPHONE, string.Empty));
        CommandManager.Instance.Play(nb);
    }

    protected new void Start()
    {
        base.Start();
    }


}
