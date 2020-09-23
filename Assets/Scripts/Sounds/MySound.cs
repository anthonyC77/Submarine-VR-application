using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class MySound 
{
    public string Name;
    public AudioClip Clip;
    [Range(0,1)]
    public float Volume;
    [Range(.1f, 3)]
    public float Pitch;
    public bool Loop = false;
    [HideInInspector]
    public AudioSource Source;
    public bool IsRandomVolume = false;
    public bool Mute = false;
    [Range(0, 1)]
    public float SpatialBlend = 0f;
    [Range(0, 256)]
    public int Priority;
}
