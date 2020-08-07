using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderWall : ColliderActions
{
    public GameObject Wall { get; set; }

    public ColliderWall(List<GameObject> listColliders, string colliderName) 
        : base(listColliders, colliderName)
    {
        Wall = GetColliderByName();
    }

    public void SetWallColor()
    {
        Wall.GetComponent<Renderer>().material.color = Helper.GetColor(ColliderName);
    }

    public void SetWallTransparent()
    {
        Wall.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

}
