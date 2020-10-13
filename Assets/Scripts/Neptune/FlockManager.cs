using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject FishPrefab;
    public int NumFish = 20;
    public GameObject[] AllFish;
    public Vector3 SwimLimits = new Vector3(3, 5, 5);
    public Vector3 GoalPos;
    public string Name = "";
   

    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)]
    public float MinSpeed;
    [Range(0.0f, 5.0f)]
    public float MaxSpeed;
    [Range(1.0f, 10.0f)]
    public float NeighbourDistance;
    [Range(0.0f, 5.0f)]
    public float RotationSpeed;
    public float DistanceBetweenFishes = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        int kikongiFish = 1;
        AllFish = new GameObject[NumFish];
        for (int i = 0; i < NumFish; i++)
        {
            Vector3 pos = GetRandomPos();
            //FishPrefab.tag = Name + i;
            AllFish[i] = (GameObject)Instantiate(FishPrefab, pos, Quaternion.identity);
            AllFish[i].GetComponent<Flock>().MyManager = this;
            

            if (!string.IsNullOrEmpty(Name))
            {
                FishPrefab.name = Name + kikongiFish;
                kikongiFish++;
            }
        }

        GoalPos = this.transform.position;
    }

    public void AddFish()
    {
        Vector3 pos = GetRandomPos();
        var fish = (GameObject)Instantiate(FishPrefab, pos, Quaternion.identity);
        fish.GetComponent<Flock>().MyManager = this;
    }

    private float GetRandom(float pos)
    {
        return Random.Range(-pos, pos);
    }

    private Vector3 GetRandomPos()
    {
        return this.transform.position + new Vector3(GetRandom(SwimLimits.x),
                                                     GetRandom(SwimLimits.y),
                                                     GetRandom(SwimLimits.z));
    }

    private void RandomLocation()
    {
        if (Random.Range(0, 100) < 10)
        {
            GoalPos = GetRandomPos();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RandomLocation();
    }
}
