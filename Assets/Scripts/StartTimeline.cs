using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    public Transform SunPos;
    public Transform PlayerPos;
    PlayableDirector timeLine;
    public GameObject TextEnterPlanet;
    
    bool begin = true;

    // Start is called before the first frame update
    void Start()
    {
        timeLine = GetComponent<PlayableDirector>();
        timeLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (begin && SunPos.position.y > 1 && PlayerPos.position.x > 0)
        {            
            timeLine.enabled = true;
            timeLine.Play();
            begin = false;
            TextEnterPlanet.SetActive(true);
        }
    }
}
