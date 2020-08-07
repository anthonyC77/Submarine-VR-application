using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlanet : ColliderActions
{
    public ColliderPlanet(AudioSource[] sounds, List<GameObject> listColliders, string colliderName)
       : base(listColliders, colliderName)
    {
    }    
}
