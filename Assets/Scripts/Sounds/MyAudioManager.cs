using UnityEngine.Audio;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MyAudioManager : MonoBehaviour
{
    public MySound[] Sounds;
    public static MyAudioManager Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }     

        DontDestroyOnLoad(gameObject);
        foreach (MySound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            //sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
            sound.Source.mute = sound.Mute;
            sound.Source.spatialBlend = sound.SpatialBlend;
            sound.Source.priority = sound.Priority;
        }
    }

    public void Start()
    {
        Play("Ouverture");
    }

    public void Play(string name, GameObject gameObject, bool isHighSound = false)
    {
        MySound s = Array.Find(Sounds, sound => sound.Name.Equals(name));
        if (s == null)
        {
            Debug.LogWarning($"Le nom {name} n'a pas été trouvé");
            return;
        }

        var audio = gameObject.AddComponent<AudioSource>();
        audio = s.Source;

        if (isHighSound)
        {
            s.Source.PlayOneShot(s.Source.clip, 1);
        }
        else
        {
            if (s.IsRandomVolume)
            {
                s.Source.PlayOneShot(s.Source.clip, Random.Range(s.Volume / 2, s.Volume));
            }
            else
            {
                s.Source.PlayOneShot(audio.clip);
            }
        }
    }

    public void Play(string name)
    {
        MySound s = Array.Find(Sounds, sound => sound.Name.Equals(name));
        if (s == null)
        {
            Debug.LogWarning($"Le nom {name} n'a pas été trouvé");
            return;
        }

        if (s.IsRandomVolume)
        {
            s.Source.PlayOneShot(s.Source.clip, Random.Range(s.Volume / 2, s.Volume));
        }
        s.Source.Play();
    }
}
