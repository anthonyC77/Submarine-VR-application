using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitBubbleSound : MonoBehaviour
{
    private ParticleSystem ParticleSystem;
    private int CountParticles;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ParticleSystem.particleCount < CountParticles) 
        {
            MyAudioManager.Instance.Play(Names.BUBBLEDIE);
        }

        if (ParticleSystem.particleCount > CountParticles)
        {
            MyAudioManager.Instance.Play(Names.BUBBLERISE);
        }

        CountParticles = ParticleSystem.particleCount;
    }
}
