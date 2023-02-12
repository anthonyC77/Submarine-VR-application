using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Navigation;

public class Explorer : MonoBehaviour
{
    public Material FractalMaterial;
    public Vector2 Pos;
    public float Scale, Angle;

    private Vector2 smoothPos;
    private float smoothScale, smoothAngle;

    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, Pos, .03f);
        smoothScale = Mathf.Lerp(smoothScale, Scale, .03f);
        smoothAngle = Mathf.Lerp(smoothAngle, Angle, .03f);

        float aspect = (float)Screen.width / (float)Screen.height;
        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect > 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;

        FractalMaterial.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        FractalMaterial.SetFloat("_Angle", smoothAngle);
    }

    private Dictionary<eDirection, bool> GetChosenDirectionKeyPad()
    {
        Dictionary<eDirection, bool> dirs = new Dictionary<eDirection, bool>();
        dirs.Add(Navigation.eDirection.IN, Input.GetKey(KeyCode.KeypadPlus));
        dirs.Add(Navigation.eDirection.OUT, Input.GetKey(KeyCode.KeypadMinus));
        dirs.Add(Navigation.eDirection.LEFT, Input.GetKey(KeyCode.LeftArrow));
        dirs.Add(Navigation.eDirection.RIGHT, Input.GetKey(KeyCode.RightArrow));
        dirs.Add(Navigation.eDirection.DOWN, Input.GetKey(KeyCode.DownArrow));
        dirs.Add(Navigation.eDirection.UP, Input.GetKey(KeyCode.UpArrow));
        dirs.Add(Navigation.eDirection.ROTATEUNCLOCK, Input.GetKey(KeyCode.PageUp));
        dirs.Add(Navigation.eDirection.ROTATECLOCK, Input.GetKey(KeyCode.PageDown));

        return dirs;
    }   

    private Dictionary<eDirection, bool> GetChosenDirectionOcculus()
    {
        var axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        
        Dictionary<eDirection, bool> dirs = new Dictionary<eDirection, bool>();
        dirs.Add(Navigation.eDirection.IN, OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger));
        dirs.Add(Navigation.eDirection.OUT, OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger));
        dirs.Add(Navigation.eDirection.LEFT, OVRInput.Get(OVRInput.Button.PrimaryHandTrigger));
        dirs.Add(Navigation.eDirection.RIGHT, OVRInput.Get(OVRInput.Button.SecondaryHandTrigger));
        dirs.Add(Navigation.eDirection.DOWN, OVRInput.Get(OVRInput.Button.One));
        dirs.Add(Navigation.eDirection.UP, OVRInput.Get(OVRInput.Button.Two));
        dirs.Add(Navigation.eDirection.ROTATEUNCLOCK, OVRInput.Get(OVRInput.Button.Three));
        dirs.Add(Navigation.eDirection.ROTATECLOCK, OVRInput.Get(OVRInput.Button.Four));

        return dirs; 
    }

    private eDirection GetChosenDirection()
    {
        var keyPress = new KeyPressByUser(GetChosenDirectionOcculus());
        if (keyPress.DirectionChosen != eDirection.NONE)
        {

        }
        return keyPress.DirectionChosen;
    }

    private void HandleInputs()
    {
        Navigation nav = new Navigation(Pos, Scale, Angle, GetChosenDirection());
        nav.SetScale();

        Scale = nav.Scale;
        Pos = nav.Pos;
        Angle = nav.Angle;
    }

    void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }
}
