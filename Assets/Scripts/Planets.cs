using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : ColliderActions
{
    public Planets(List<GameObject> listColliders, string colliderName)
        : base(listColliders, colliderName)
    {
        GetColliderNameByNoteName();
    }

    public string PlanetName { get; set; }
    public GameObject Planet { get; set; }

    private void GetColliderNameByNoteName()
    {
        var note = Helper.GetNoteCalled(ColliderName);

        switch (note)
        {
            case eNote.NONE:
                break;
            case eNote.Fa_1:
                PlanetName = ePlanet.Jupiter.ToString();
                break;
            case eNote.Sol_2:
                PlanetName = ePlanet.Mercure.ToString();
                break;
            case eNote.La_3:
                PlanetName = ePlanet.Terre.ToString();
                break;
            case eNote.Mi_4:
                PlanetName = ePlanet.Venus.ToString();
                break;
            case eNote.Si_6:
                PlanetName = ePlanet.Saturne.ToString();
                break;
            case eNote.Do_7:
                PlanetName = ePlanet.Neptune.ToString();
                break;
            case eNote.Re_8:
                PlanetName = ePlanet.Mars.ToString();
                break;
            case eNote.SiBemol_9:
                PlanetName = ePlanet.Uranus.ToString();
                break;
            default:
                break;
        }
    }

    public void SetMaterial()
    {
        Planet = GetColliderByName(PlanetName);
        Planet.GetComponent<Renderer>().material = Helper.GetPlanetMaterial(PlanetName);
    }
}
