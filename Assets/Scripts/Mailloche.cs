using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailloche : MonoBehaviour
{
    List<GameObject> Walls = new List<GameObject>();
    List<GameObject> Notes = new List<GameObject>();
    List<GameObject> Planets = new List<GameObject>();
    private GameObject Sun;
    private GameObject Indications;
    List<string> PlanetsColored = new List<string>();
    public float ThrowForce = 10;
    public OVRCameraRig Camera;
    bool SunRising = false;
    ColliderNote Note;
    Recorder recorder;
    GameObject CountBall;
    GameObject Aiguille;
    List<Renderer> MinuteBallsRenderers;
    bool rec = false;
    public GameObject HeadPhone;
    public List<GameObject> Chakras;

    public GameObject Kikongi;
    
    private void SetPlanetsPosition()
    {
        foreach (var planet in Helper.GetEnumList<ePlanet>())
        {
            InstantiatePlanet(planet);
        }
    }

    void InstantiatePlanet(ePlanet planet)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        PlanetPosition planetPosition = new PlanetPosition(planet, ePositionType.INIT);
        sphere.transform.localScale = planetPosition.Scale;
        sphere.transform.position = planetPosition.Position;
        sphere.name = planetPosition.Name;
        sphere.tag = TagNames.PLANETS;
        sphere.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Transparent", typeof(Material));
        var rigidBody = sphere.AddComponent<Rigidbody>();
        rigidBody.useGravity = true;
        rigidBody.constraints = RigidbodyConstraints.FreezePositionX
                              | RigidbodyConstraints.FreezePositionY
                              | RigidbodyConstraints.FreezePositionZ;
        var pos = new PlanetPosition(planet, ePositionType.WALL);
        rigidBody.AddForce(pos.Position, ForceMode.Force);

        var grab = sphere.AddComponent<OVRGrabbable>();
        grab.enabled = true;
        grab.M_GrabPoints = new Collider[1] { sphere.GetComponent<SphereCollider>() } ;
        var planetScript = sphere.AddComponent<Planet>();
        planetScript.enabled = true;
        sphere.GetComponent<SphereCollider>().isTrigger = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlanetsPosition();
        Walls = GameObject.FindGameObjectsWithTag(TagNames.WALLS).ToList();
        Notes = GameObject.FindGameObjectsWithTag(TagNames.NOTES).ToList();
        Planets = GameObject.FindGameObjectsWithTag(TagNames.PLANETS).ToList();
        Sun = GameObject.FindGameObjectWithTag(TagNames.SUN);
        Indications = GameObject.FindGameObjectWithTag(TagNames.INDICATIONS);
        CountBall = GameObject.FindGameObjectWithTag(TagNames.COUNTBALL);
        Aiguille = Helper.FindByTag(TagNames.CIRCLERED);
        Indications.SetActive(false);
        recorder = new Recorder();
        MinuteBallsRenderers = Helper.FindChildrensByTag<Renderer>(TagNames.MINUTE);
        Chakras = Helper.FindByTags(TagNames.CHAKRA);
    }

    // Update is called once per frame
    void Update()
    {
        RiseSun();
        StartStopEmissionPlanetBySound();

        
    }
    float sec = 1;
    float min = 1;
    IEnumerator time()
    {        
        if (recorder.PrecTypeActionRecorder == eTypeActionRecorder.Rec)
        {
            while (sec < 481 && rec)
            {
                Aiguille.transform.Rotate(new Vector3(0, 6, 0));

                int idBall = (int)(sec / 60);
                var limit = idBall * 60;
                if (idBall > 0)
                {
                    var getCurrentBall = MinuteBallsRenderers.Where(b => b.name.Equals(TagNames.MINUTE + idBall)).FirstOrDefault();

                    if (sec == limit + 1)
                    {
                        getCurrentBall.material = Helper.GetRecorderMaterial(TagNames.RED);
                    }
                }
                

                sec++;

                if (sec == 480)
                {
                    recorder.StopAndRec();
                    foreach (Renderer renderer in MinuteBallsRenderers)
                    {
                        renderer.material = Helper.GetRecorderMaterial((TagNames.GREEN));
                    }
                    Aiguille.transform.rotation = new Quaternion(0, 0, 0, 0);
                }

                yield return new WaitForSeconds(1);
            } 
        }        
    }

    private void StartStopEmissionPlanetBySound()
    {
        if (Note != null)
        {
            if (Note.PlayingSound.isPlaying)
            {
                Note.EmissionPlanet.enabled = true;
            }
            else
            {
                Note.EmissionPlanet.enabled = false;
                Note = null;
            }
        }
    }

    private void RiseSun()
    {
        var sunTransform = Sun.transform;
        if (SunRising)
        {
            if (sunTransform.position.y < 1.5f)
            {
                sunTransform.Translate(new Vector3(0, Time.deltaTime * 0.8f, 0));
            }
            else
                Indications.SetActive(true);
        }
    }

    private void PlayHeadPhone(string name)
    {
        int nb = int.Parse(name.Replace(TagNames.HEADPHONE, string.Empty));
        CommandManager.Instance.Play(nb);
    }

    private void OnTriggerEnter(Collider other)
    {
        string colliderName = other.name;

        if (Helper.IsNoteCalled(colliderName))
        {
            //ActionsWithWalls(colliderName);
            ActionsWithKikongiSounds(colliderName);
            ActionOnPlanets(colliderName);
        }

        if (colliderName.StartsWith(TagNames.HEADPHONE))
        {
            PlayHeadPhone(colliderName);
        }

        if (Recorder.ContainsRecorder(other.name))
        {
            recorder.DoAction(other.gameObject);

            if (other.name.Equals(TagNames.REC))
            {
                rec = true;
                StartCoroutine(time());                
            }
            else
            {
                // reinit balls and pendule
                rec = false;
                StopCoroutine(time());
            }

            if (recorder.Recorded)
            {
                CreatePrefabHeadphone(recorder.IdFileSaved);
            }
        }        
    }   

    private void CreatePrefabHeadphone(int id)
    {
        float posx = -0.176f - 100 * id;
        float posz = 0.425f - 100 * id;
        HeadPhone.transform.position = new Vector3(posx, 0.395f, posz);
        HeadPhone.GetComponentInChildren<TMPro.TextMeshPro>().text = TagNames.MUSIC + " " + id;
        // set position
        var clone = Instantiate(HeadPhone);
        clone.name = TagNames.HEADPHONE + id;
    }
    
    private void ActionsWithWalls(string colliderName)
    {
        var colliderWall = new ColliderWall(Walls, colliderName);
        colliderWall.SetWallColor();
    }

    private void ActionsWithKikongiSounds(string colliderName)
    {
        var soundsKikongi = Kikongi.GetComponentsInChildren<AudioSource>();
        Note = new ColliderNote(soundsKikongi, Notes, colliderName, Sun);
        Note.Play();
    }    

    private void ActionOnPlanets(string colliderName)
    {
        var planet = new Planets(Planets, colliderName);  
        planet.SetMaterial();
        AddPlanetsColored(planet.Planet.name);
        planet.Planet.transform.localScale.Scale(new Vector3(0.5f, 0.5f, 0.5f));
        ActionOnPlayer(planet.Planet.name);
    }

    private void ActionOnPlayer(string planetName )
    {
        var chakra = Chakras.Where(c => c.name.Equals(TagNames.CHAKRA + planetName)).FirstOrDefault();
        var emissionChakra = chakra.GetComponentInChildren<ParticleSystem>().emission;
        emissionChakra.enabled = true;
    }

    private void AddPlanetsColored(string name)
    {
        if (!SunRising)
        {
            if (!PlanetsColored.Contains(name))
            {
                PlanetsColored.Add(name);
            }

            if (PlanetsColored.Count.Equals(8))
            {
                SunRising = true;
            }
        }
    }
}
