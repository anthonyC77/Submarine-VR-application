using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation 
{
    public float Scale;
    public float Angle;
    public Vector2 Pos;
    private float difference = .01f;
    private eDirection KeyChosen;

    public Navigation(Vector2 pos, float scale, float angle, eDirection keyChosen)
    {
        Pos = pos;
        Scale = scale;
        Angle = angle;
        KeyChosen = keyChosen;
    }

    private Vector2 SetDirVector()
    {
        var dir = new Vector2(difference * Scale, 0);
        float s = Mathf.Sin(Angle);
        float c = Mathf.Cos(Angle);
        return new Vector2(dir.x * c, dir.x * s);
    }

    public void SetScale()
    {
        switch (KeyChosen)
        {
            case eDirection.ROTATECLOCK:
                Angle = SetValue(-difference, Angle);
                break;
            case eDirection.ROTATEUNCLOCK:
                Angle = SetValue(difference, Angle);
                break;
            case eDirection.IN:
                SetScaleZoom(1 - difference);
                break;
            case eDirection.OUT:
                SetScaleZoom(1 + difference);
                break;
            case eDirection.LEFT:
                SetValueDir(true);
                break;
            case eDirection.RIGHT:
                SetValueDir(false);
                break;
            case eDirection.UP:
                SetValueDir(false, true);
                break;
            case eDirection.DOWN:
                SetValueDir(true, true);
                break;
            default:
                break;
        }
    }

    private void SetScaleZoom(float value)
    {
        Scale *= value;
    }

    private float SetValue(float value, float pos)
    {
        pos = pos + (value * Scale);
        return pos;
    }

    private void SetValueDir(bool sign, bool invert = false )
    {
        var dir = SetDirVector();
        if (invert)
        {
            dir = new Vector2(-dir.y, dir.x);
        }

        if (sign)
        {
            Pos -= dir;
        }
        else
        {
            Pos += dir;
        }        
    }

    public enum eDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        IN,
        OUT,
        ROTATECLOCK,
        ROTATEUNCLOCK,
        NONE
    }

}
