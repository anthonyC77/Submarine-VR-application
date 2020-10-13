using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWithDolphin : MonoBehaviour
{
    List<GameObject> PlayZones = new List<GameObject>();
    public GameObject Dolphin;
    public bool Activate = false;
    public int Speed = 2;

    private void Awake()
    {
        PlayZones = Helper.FindByTags(Names.DOLPHINPLAYZONE);
    }

    private void Update()
    {
        if (Activate)
        {
            CalculDistance();
        }
    }

    bool calcul = false;
    private float timeCount = 0.0f;
    bool changeDirection = false;
    GameObject toGo = null;
    GameObject toGoAfter = null;
    Vector3 positionToGo;
    int angle = 0;

    private void CalculDistance()
    {
        float dist = 1000;
        int posToGo = -1;        

        if (!calcul)
        {
            for (int i = 0; i < 2; i++)
            {
                var zone = PlayZones[i];
                var distTemp = Vector3.Distance(zone.transform.position, Dolphin.transform.position);

                if (distTemp < dist)
                {
                    positionToGo = zone.transform.position;
                    dist = distTemp;
                    toGo = zone;
                    posToGo = i;
                }
            }
            toGoAfter = PlayZones[0];
            if (posToGo == 0)
            {
                toGoAfter = PlayZones[1];
                angle = 180;
            }

            calcul = true;
        }

        var distDolphinToGo = Vector3.Distance(positionToGo, Dolphin.transform.position);

        if (distDolphinToGo < 2 && !changeDirection)
        {
            changeDirection = true;
            Dolphin.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        if (changeDirection)
        {
            DolphinTo(toGoAfter, false);
            Debug.Log("Closer to " + toGoAfter.name);
        }
        else
        {
            DolphinTo(toGo);
            Debug.Log("Closer to " + toGo.name);
        }
    }

    private void DolphinTo(GameObject toGo, bool rotate = true)
    {
        if (rotate)
        {
            Dolphin.transform.rotation = Quaternion.Slerp(Dolphin.transform.rotation,
                Quaternion.LookRotation(toGo.transform.position), Speed * Time.deltaTime);

            //Dolphin.transform.rotation = Quaternion.Slerp(Dolphin.transform.rotation,
            //toGo.transform.rotation, timeCount);
            //timeCount = timeCount + Time.deltaTime;
        }
        
        Dolphin.transform.position =
           Vector3.MoveTowards(Dolphin.transform.position, toGo.transform.position, Time.deltaTime * Speed);
    }
}
