using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public AudioSource FallingCubes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Names.ICECUBE))
        {
            FallingCubes.PlayOneShot(FallingCubes.clip);
        }
    }
}
