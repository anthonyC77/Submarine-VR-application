using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;
using System;

public class CommandManager : MonoBehaviour
{
    private static CommandManager instance;
    private DateTime start;
    public bool StartRoutine { get; set; }
    private bool isRecording = false;
    private bool isPlaying = false;
    

    public static CommandManager Instance
    {
        get
        {
            if (instance == null)
            {
                //System.Diagnostics.Debug.LogError("CommandManager is null!");
            }

            return instance;
        }
    }

    private List<ICommand> CommandBuffer = new List<ICommand>();

    private void Awake()
    {
        instance = this;        
    }

    private void Load(int id)
    {
        SavePlayInFile.ReadFromXml(id);
    }

    public void Stop()
    {
        isRecording = false;
    }

    public void Delete(int id)
    {
        SavePlayInFile.DeleteFromXml(id);
    }

    public int GetAllFiles()
    {
        return SavePlayInFile.GetNbFiles();
    }

    public void Start()
    {        
        isRecording = true;
    }

    public void AddCommand(ICommand command)
    {
        if (isRecording || isPlaying)
        {
            if (!StartRoutine)
            {
                command.DatePlay = DateTime.Now;
                CommandBuffer.Add(command);
            }
        }
    }

    public void StopReading()
    {
        isRecording = false;
        CommandBuffer.Clear();
    }

    public void Play(int id)
    {
        isPlaying = true;
        SavePlayInFile.ReadFromXml(id);
        isPlaying = false;
        StartCoroutine(DoRoutine(false));
    }

    public void Rewind()
    {
        StartCoroutine(DoRoutine(true));
    }

    public int SaveInFile()
    {
        int id = SavePlayInFile.SaveinXml(CommandBuffer);
        CommandBuffer.Clear();
        return id;
    }

    IEnumerator DoRoutine(bool reverse)
    {
        StartRoutine = true;
        var list = CommandBuffer.AsEnumerable();

        if (reverse)
        {
            list = Enumerable.Reverse(CommandBuffer);
        }

        var time = list.First().DatePlay.AddSeconds(-1);

        foreach (var command in list)
        {
            try
            {
                command.Execute();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            var elpasedTime = command.DatePlay - time;
            float elapse = GetTime(elpasedTime);

            yield return new WaitForSeconds(elapse);
        }

        StartRoutine = false;
    }

    private float GetTime(TimeSpan time)
    {
        return float.Parse(time.TotalSeconds.ToString());
    }
}
