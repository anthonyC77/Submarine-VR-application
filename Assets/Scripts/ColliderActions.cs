using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActions
{
    List<GameObject> ListColliders;
    public string ColliderName { get; set; }

    public ColliderActions(List<GameObject> listColliders, string colliderName)
    {
        ListColliders = listColliders;
        ColliderName = colliderName;

    }

    public GameObject GetColliderByName()
    {
        return GetColliderByName(ColliderName);
    }

    public GameObject GetColliderByName(string name)
    {
        return ListColliders.Where(w => w.name.Equals(name)).FirstOrDefault();
    }
}
