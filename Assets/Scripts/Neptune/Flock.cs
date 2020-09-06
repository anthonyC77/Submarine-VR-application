using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager MyManager;
    float Speed;
    bool Turning = false;

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(MyManager.MinSpeed, MyManager.MaxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        BoundRules();

        transform.Translate(0, 0, Time.deltaTime * Speed);
    }

    void BoundRules()
    {
        Bounds b = new Bounds(MyManager.transform.position, MyManager.SwimLimits * 2);
        Turning = !b.Contains(transform.position);

        if (Turning)
        {
            Vector3 direction = MyManager.transform.position - transform.position; // center of the cube
            transform.rotation = transform.rotation = Rotation(direction);
        }
        else
        {
            SpeedRandom();
            RulesRandom();
        }
    }

    void BoundRulesPillar()
    {
        Bounds b = new Bounds(MyManager.transform.position, MyManager.SwimLimits * 2);
        RaycastHit hit = new RaycastHit();
        Vector3 direction = Vector3.zero;

        if (!b.Contains(transform.position))
        {
            Turning = true;
            direction = MyManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            Turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
            Turning = false;

        if (Turning)
        {
            transform.rotation = Rotation(direction);
        }
        else
        {
            SpeedRandom();
            RulesRandom();
        }
    }

    void SpeedRandom()
    {
        if (Random.Range(0, 100) < 10)
            Speed = Random.Range(MyManager.MinSpeed, MyManager.MaxSpeed);
    }

    void RulesRandom()
    {
        if (Random.Range(0, 100) < 20)
            ApplyRules();
    }

    private Quaternion Rotation(Vector3 direction)
    {
        return transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      MyManager.RotationSpeed * Time.deltaTime);
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = MyManager.AllFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gspeed = 0.01f;
        float ndistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                ndistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (ndistance <= MyManager.NeighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (ndistance < MyManager.DistanceBetweenFishes)
                    {
                        vavoid += (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gspeed += anotherFlock.Speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (MyManager.GoalPos - this.transform.position);
            Speed = gspeed / groupSize;
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Rotation(direction);
            }
        }
    }
}
