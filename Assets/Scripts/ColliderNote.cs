using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ColliderNote : ColliderActions
{
    public AudioSource PlayingSound;
    public EmissionModule EmissionPlanet; 
    public AudioSource[] Sounds { get; set; }
    private GameObject Note;
    private GameObject Sun;

    public ColliderNote(AudioSource[] sounds, List<GameObject> listColliders, string colliderName, GameObject sun)
        : base(listColliders, colliderName)
    {
        Sounds = sounds;
        Note = GetColliderByName();
        Sun = sun;
    }

    public void Play()
    {
        var instanceCommandManager = CommandManager.Instance;
        EmitParticle();
        PlayingSound = GetSoundToPlay();
        var note = Helper.GetNoteCalled(Note.name);
        LightPLanetTrajectoire(note);
        var playCommand = new PlayNoteKikongiCommand(PlayingSound);        
        playCommand.Execute();
        instanceCommandManager.AddCommand(playCommand);
    }

    private AudioSource GetSoundToPlay()
    {
        return Sounds.Where(n => n.name.Equals(ColliderName)).FirstOrDefault();
    }

    private void EmitParticle()
    {
        
        var particle = Note.GetComponentInChildren<ParticleSystem>();
        var main = particle.main;
        //main.duration = 2;
        var emission = particle.emission;
        emission.enabled = true;
    }

    private void LightPLanetTrajectoire(eNote note)
    {
        var planet = Helper.GetPlanetByNote(note);

        var particle = Sun.GetComponentsInChildren<ParticleSystem>()
            .Where(p => p.name.Equals(planet.ToString())).FirstOrDefault();
        EmissionPlanet = particle.emission;
        EmissionPlanet.enabled = true;
    }
}
