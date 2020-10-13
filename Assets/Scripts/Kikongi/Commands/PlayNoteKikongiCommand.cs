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
    [SerializeField]
    public float HitVol { get; set; }
    private float lowPitchRange = .75f;
    private float highPitchRange = 1.5f;
    private float velToVol = .2f;
    private float velocityClipSplit = 10f;
    private Collision CollisionNote;

    public PlayNoteKikongiCommand(AudioSource surfaceKikongi)
    {
        SurfaceKikongi = surfaceKikongi;
        NoteName = surfaceKikongi.name;
        HitVol = 1;
    }

    public PlayNoteKikongiCommand(AudioSource surfaceKikongi, Collision collision)
    {
        SurfaceKikongi = surfaceKikongi;
        NoteName = surfaceKikongi.name;
        CollisionNote = collision;
        //SurfaceKikongi.pitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

        float magnitude = CollisionNote.relativeVelocity.sqrMagnitude;
        if (magnitude == 0)
        {
            magnitude += 1f;
        }
        HitVol = magnitude * velToVol;
        
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
        // todo sound fort and low
        //if (CollisionNote.relativeVelocity.sqrMagnitude < velocityClipSplit)
        //    SurfaceKikongi.PlayOneShot(SurfaceKikongi.clip, HitVol);
        //else
        SurfaceKikongi.PlayOneShot(SurfaceKikongi.clip, HitVol);
    }
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("HitVol", HitVol, typeof(float));
        info.AddValue("DatePlay", DatePlay, typeof(DateTime));
        info.AddValue("NoteName", NoteName, typeof(string));    
    }
}
