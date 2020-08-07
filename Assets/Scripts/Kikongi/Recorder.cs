using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum eTypeActionRecorder
{
    Read,
    Rec,
    Stop,
}


public class Recorder 
{    
    private float clickablePos = 0.0300000f;
    private List<Transform> TransformsButton;
    private List<GameObject> Buttons;
    private GameObject ButtonRead;
    private GameObject ButtonRec;
    private GameObject ButtonStop;
    public eTypeActionRecorder PrecTypeActionRecorder;

    public Recorder()
    {
        ButtonRead = Helper.FindByTag(TagNames.READ);
        ButtonRec = Helper.FindByTag(TagNames.REC);
        ButtonStop = Helper.FindByTag(TagNames.STOP);
        Init();        
    }

    public static bool ContainsRecorder(string name)
    {
        return new string[] { TagNames.READ, TagNames.REC, TagNames.STOP }.Contains(name);
    }

    public void DoAction(GameObject buttonSelected)
    {
        string name = buttonSelected.name;
        bool? stopClickable = null;

        var typeActionRecorder = Helper.GetEnumValueByName<eTypeActionRecorder>(name);

        switch (typeActionRecorder)
        {
            case eTypeActionRecorder.Read:
                ActionOnButton(false, ButtonRec);
                stopClickable = true;
                SetButton(false, buttonSelected);
                CommandManager.Instance.Play();
                PrecTypeActionRecorder = eTypeActionRecorder.Read;
                break;
            case eTypeActionRecorder.Rec:
                Color.Lerp(Color.blue, Color.cyan, 10);

                CommandManager.Instance.Start();
                ActionOnButton(false, ButtonRead);
                stopClickable = true;
                SetButton(false, buttonSelected);
                PrecTypeActionRecorder = eTypeActionRecorder.Rec;
                break;
            case eTypeActionRecorder.Stop:
                CommandManager.Instance.Stop();
                stopClickable = false;
                ActionAfterStop();
                break;
            default:
                break;
        }

        ActionOnButton(stopClickable, ButtonStop);
        
    }

    private void ActionAfterStop()
    {
        switch (PrecTypeActionRecorder)
        {
            case eTypeActionRecorder.Read:
                CommandManager.Instance.StopReading();
                break;
            case eTypeActionRecorder.Rec:                
                CommandManager.Instance.SaveInFile();
                break;
            case eTypeActionRecorder.Stop:
                break;
            default:
                break;
        }

        ActionOnButton(true, ButtonRead);
        ActionOnButton(true, ButtonRec);
    }

    private void ActionOnButton(bool? stopClickable, GameObject button)
    {
        if (stopClickable.HasValue)
        {
            SetButton(stopClickable.Value, button);
        }
    }

    private void Init()
    {
        bool hasRecords = HasRecords();
        SetButton(hasRecords, ButtonRead);

        SetButton(true, ButtonRec);
    }

    private bool HasRecords()
    {
        // todo manager
        return true;
    }

    private void SetButton(bool clickable, GameObject button)
    {
        float nextPosY = GetNexPos(clickable);
        var pos = button.transform.position;
        pos.y += nextPosY;
        button.transform.position = pos;
    }
    
    private float GetNexPos(bool clickable)
    {
        if (clickable)
        {
            return clickablePos ;
        }
        else
        {
            return -clickablePos;
        }
    }
}
