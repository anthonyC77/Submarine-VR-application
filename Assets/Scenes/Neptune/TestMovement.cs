using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public GameObject Destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position =
            Vector3.MoveTowards(transform.position, Destination.transform.position, Time.deltaTime * 0.1f);
    }
}
