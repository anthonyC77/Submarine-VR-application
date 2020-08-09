using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum eTypeActionRecorder
{
    ChooseRead,
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
    public int IdFileSaved = 0;
    public bool Recorded = false;

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

    public void Reading(int id)
    {
        CommandManager.Instance.Play(id);
    }

    public void DoAction(GameObject buttonSelected)
    {
        Recorded = false;
        string name = buttonSelected.name;
        bool? stopClickable = null;

        var typeActionRecorder = Helper.GetEnumValueByName<eTypeActionRecorder>(name);

        switch (typeActionRecorder)
        {
            case eTypeActionRecorder.ChooseRead:
                ActionOnButton(false, ButtonRec);
                stopClickable = true;
                SetButton(false, buttonSelected);                
                PrecTypeActionRecorder = eTypeActionRecorder.ChooseRead;
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
                stopClickable = false;
                ActionAfterStop();
                break;
            default:
                break;
        }

        ActionOnButton(stopClickable, ButtonStop);
        
    }

    public void StopAndRec()
    { 
        CommandManager.Instance.Stop();
        ActionOnButton(false, ButtonStop);
    }

    private void ActionAfterStop()
    {
        Recorded = false;
        CommandManager.Instance.Stop();
        switch (PrecTypeActionRecorder)
        {
            case eTypeActionRecorder.ChooseRead:
                CommandManager.Instance.StopReading();
                break;
            case eTypeActionRecorder.Rec:
                IdFileSaved = CommandManager.Instance.SaveInFile();
                Recorded = true;
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
