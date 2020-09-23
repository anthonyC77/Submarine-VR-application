using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<Material>().color = Color.red;
    }
}
