using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class AddRandomPlants
{
    public static void Put(List<GameObject> plants, Transform parentPlantPos)
    {
        foreach (var plant in plants)
        {
            Debug.Log("Plant spawned : " + plant.name);
            plant.transform.rotation = new Quaternion(-90, 0, 0, 0);
            var seaBed = Helper.FindByTag(Names.SEABED);
            var posSeaBed = seaBed.transform.position;
            Vector3 posPlant = GetRandomPos(posSeaBed);
            var plantInstance = (GameObject)GameObject.Instantiate(plant, posPlant, Quaternion.identity);
            plantInstance.transform.SetParent(parentPlantPos);
        }        
    }

    private static Vector3 GetRandomPos(Vector3 posSeaBed)
    {
        return new Vector3(GetRandom(50),
                           0,
                           GetRandom(50));
    }

    private static float GetRandom(float pos)
    {
        return Random.Range(-pos, pos);
    }
}
