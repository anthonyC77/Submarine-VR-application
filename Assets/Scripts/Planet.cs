using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour
{
    List<GameObject> Walls = new List<GameObject>();
    List<GameObject> Notes = new List<GameObject>();
    List<GameObject> Planets = new List<GameObject>();
    List<GameObject> PosRotPlanets = new List<GameObject>();
    private GameObject WallTouchedBySphere;
    private GameObject Sun;
    bool GettingWallTransparent = false;
    bool WallIsTransparent = false;
    Vector3 InitPosPlanet;
    public int RotSpeed = 33;
    bool RotationMode = false;
    public int Speed = 10;

    private void Awake()
    {
        Walls = Helper.FindByTags(TagNames.WALLS);
        Notes = Helper.FindByTags(TagNames.NOTES);
        Planets = Helper.FindByTags(TagNames.PLANETS);
        PosRotPlanets = Helper.FindByTags(TagNames.POSROTPLANET);
        InitPosPlanet = this.transform.position;
        Sun = Helper.FindByTag(TagNames.SUN);
    }

    IEnumerator FadeWallColor(float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            Color c = WallTouchedBySphere.GetComponent<Renderer>().material.color;
            c.a = c.a - 0.0001f;
            WallTouchedBySphere.GetComponent<Renderer>().material.color = c;
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (RotationMode)
        {
            this.gameObject.transform.Rotate(0, Time.deltaTime * RotSpeed, 0, Space.World);
            Vector3 axisFromSun = Sun.transform.up;
            this.gameObject.transform.RotateAround(Sun.transform.position, Sun.transform.up, Speed * Time.deltaTime);
        }

        if (GettingWallTransparent)
        {
            if (WallIsTransparent)
            {
                GettingWallTransparent = false;                
            }
            else
            {
                Color color = WallTouchedBySphere.GetComponent<Renderer>().material.color;
                
                if (color.a <= 0)
                {
                    WallIsTransparent = true;
                }
                else
                {
                    WallTouchedBySphere.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Transparent", typeof(Material));
                    StartCoroutine(FadeWallColor(2));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        //if (Helper.IsNoteCalled(colliderName))
        //{
        //    ActionsWithWalls(colliderName);
        //    SetPosPlanetAfterWall();
        //}

        if (other.tag.Equals(TagNames.SUNCOLLISION))
        {
            SetPosPlanetAroundTheSun();
        }
    }

    private void ActionsWithWalls(string colliderName)
    {
        var colliderWall = new ColliderWall(Walls, colliderName);
        WallTouchedBySphere = colliderWall.Wall;
        GettingWallTransparent = true;        
    }

    private void SetPosPlanetAfterWall()
    {
        ePlanet planet = Helper.GetEnumValueByName<ePlanet>(this.name);
        PlanetPosition planetPosition = new PlanetPosition(planet, ePositionType.WALL);
        this.transform.position = planetPosition.Position;
        RotationMode = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }


    private void SetPosPlanetAroundTheSun()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        var grab = this.GetComponent<OVRGrabbable>();
        grab.enabled = true;
        grab.M_GrabPoints = null;
        ePlanet planet = Helper.GetEnumValueByName<ePlanet>(this.name);
        PlanetPosition planetPosition = new PlanetPosition(planet, ePositionType.AROUNDSUN);
        this.transform.position = planetPosition.Position;
        RotationMode = true;        
    }

    
}
