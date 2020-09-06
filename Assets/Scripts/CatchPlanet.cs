using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatchPlanet : MonoBehaviour
{
    List<GameObject> Planets;

    private void Awake()
    {
        Planets = GameObject.FindGameObjectsWithTag(Names.PLANETS).ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Planets.Contains(other.gameObject))
        {
            // get wall and go down by coroutine
        }   
    }


}
