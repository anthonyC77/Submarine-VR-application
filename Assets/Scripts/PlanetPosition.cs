using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPosition 
{
    public float posY = 1.2f;
    public float unitPosOnColumn = .25f;
    public float sphereScale = 0.1f;
    public float unitPosAfterWall = 4.5f;
    public float unitPosSun = 1.5f;

    public Vector3 Scale { get; set; }
    public Vector3 Position { get; set; }
    private ePlanet Planet;
    private ePositionType Positiontype;
    public string Name { get; set; }

    public PlanetPosition(ePlanet planet, ePositionType positiontype)
    {
        Planet = planet;
        Positiontype = positiontype;
        Name = planet.ToString();
        SetPositionPlanet();
    }

    private void SetPositionPlanet()
    {
        switch (Positiontype)
        {
            case ePositionType.INIT:
                InstantiatePlanetOnColumn();
                break;
            case ePositionType.WALL:
                InstantiatePlanetAfterWall();
                break;
            case ePositionType.SPACE:
                // todo after
                break;
            case ePositionType.AROUNDSUN:
                InstantiatePlanetAroundTheSun();
                break;
            default:
                break;
        }
    }
    
    private void InstantiatePlanetAfterWall()
    {
        InstantiatePlanet(unitPosAfterWall);
    }

    private void InstantiatePlanetOnColumn()
    {
        InstantiatePlanet(unitPosOnColumn);
    }

    private void InstantiatePlanetAroundTheSun()
    {
        Scale = new Vector3(sphereScale, sphereScale, sphereScale);
        float posX = 0;

        switch (Planet)
        {
            case ePlanet.Mercure:
                posX = 0.6f;
                break;
            case ePlanet.Venus:
                posX = 1.1f;
                break;
            case ePlanet.Terre:
                posX = 1.5f;
                break;
            case ePlanet.Mars:
                posX = 2.3f;
                break;
            case ePlanet.Jupiter:
                posX = 4;
                break;
            case ePlanet.Saturne:
                posX = 4.5f;
                break;
            case ePlanet.Uranus:
                posX = 5;
                break;
            case ePlanet.Neptune:
                posX = 5.5f;
                break;
            default:
                break;
        }
        posX = posX / 2;
        Position = new Vector3(posX, posY, 0);
    }

    private void InstantiatePlanet(float unitPos)
    {
        Scale = new Vector3(sphereScale, sphereScale, sphereScale);
        float posX = 0;
        float posZ = 0;

        switch (Planet)
        {
            case ePlanet.Venus:
                posX = unitPos;
                posZ = -unitPos;
                break;
            case ePlanet.Terre:
                posX = 0;
                posZ = unitPos;
                break;
            case ePlanet.Mars:
                posX = -unitPos;
                posZ = -unitPos;
                break;
            case ePlanet.Jupiter:
                posX = unitPos;
                posZ = 0;
                break;
            case ePlanet.Neptune:
                posX = 0;
                posZ = -unitPos;
                break;
            case ePlanet.Saturne:
                posX = -unitPos;
                posZ = unitPos;
                break;
            case ePlanet.Mercure:
                posX = unitPos;
                posZ = unitPos;
                break;
            case ePlanet.Uranus:
                posX = -unitPos;
                posZ = 0;
                break;
            default:
                break;
        }

        Position = new Vector3(posX, posY, posZ);
    }

}
