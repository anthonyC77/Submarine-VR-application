using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.Serialization;

[Serializable]
public class PlayNoteKikongiCommand : ICommand, ISerializable
{
    private AudioSource[] SurfacesKikongi;
    private AudioSource SurfaceKikongi;
    [SerializeField]
    public DateTime DatePlay { get; set; }
    [SerializeField]
    public string NoteName { get; set; }

    public PlayNoteKikongiCommand(AudioSource surfaceKikongi)
    {
        SurfaceKikongi = surfaceKikongi;
        NoteName = surfaceKikongi.name;
    }

    public PlayNoteKikongiCommand(AudioSource[] surfacesKikongi, eNote noteName)
    {
        SurfacesKikongi = surfacesKikongi;
        NoteName = noteName.ToString();
        SurfaceKikongi = SurfacesKikongi
            .Where(s => s.name.Equals(NoteName.ToString()))
            .FirstOrDefault();
    }

    public void Execute()
    {
        SurfaceKikongi.PlayOneShot(SurfaceKikongi.clip);
    }
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("DatePlay", DatePlay, typeof(DateTime));
        info.AddValue("NoteName", NoteName, typeof(string));    
    }
}
