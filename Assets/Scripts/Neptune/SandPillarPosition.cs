using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPillarPosition : MonoBehaviour
{
    public int PillarCountByLine = 4;
    public GameObject Pillar;
    public float SpaceBetweenPillar = 10;
    private void Awake()
    {
        for (int i = 1; i < PillarCountByLine + 1; i++)
        {
            for (int line = 0; line < 2; line++)
            {
                for (int column = 0; column < 2; column++)
                {

                }
            }
        }
    }
}
