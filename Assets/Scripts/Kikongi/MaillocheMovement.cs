using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaillocheMovement : MonoBehaviour
{
    public GameObject Kikongi;
    public GameObject Animals;
    public FlockManager Manager;
    public List<GameObject> Plants;
    public GameObject DolphinCircle;
    public int maxPlaysForDolphinAndWhale = 20;

    List<GameObject> Notes = new List<GameObject>();
    Vector3 posMailloche;    
    bool animalVisible = false;
    bool? hasMovedFishes = null;
    int fishToAdd = 0;    

    private void Awake()
    {
        Notes = GameObject.FindGameObjectsWithTag(Names.NOTES).ToList();
    }

    string colliderName = string.Empty;
    Vector3 posMaillocheBeforePlay = new Vector3();
    bool playNote = false;

    IEnumerator ReplaceMailloche()
    {
        playNote = true;
        yield return new WaitForSeconds(0.2f);
        ActionsWithKikongiSoundsOnly(colliderName);
        colliderName = string.Empty;
        yield return new WaitForSeconds(0.1f);

        //if (Vector3.Distance(this.transform.position, posMaillocheBeforePlay) < 1)
        //{
        //    this.transform.position = posMaillocheBeforePlay;
        //}
    }

    private void AddPlants()
    {
        if (Plants != null && Plants.Count > 0)
        {
            AddRandomPlants.Put(Plants, Animals.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //posMailloche = this.gameObject.transform.position;
        if (collision.gameObject.tag == Names.KIKONGI)
        {
            // sound metallic kikongi
            //Debug.Log("Play kikongi metal");
            //var audio = collision.gameObject.GetComponent<AudioSource>();
            //audio.PlayOneShot(audio.clip);
            if (!string.IsNullOrEmpty(colliderName) && !playNote)
            {
                AddPlants();
                fishToAdd++;
                StartCoroutine(ReplaceMailloche());
            }
        }
        else if (collision.collider.name.StartsWith(Names.NOTE))
        {
            if (!animalVisible)
            {
                if (Animals != null)
                {
                    Animals.SetActive(true);
                    animalVisible = true;
                }                
            }
            else
            {
                if (!hasMovedFishes.HasValue)
                {
                    hasMovedFishes = false;
                }                
            }

            playNote = false;
            colliderName = collision.collider.name;
            posMaillocheBeforePlay = new Vector3(
                this.transform.position.x, this.transform.position.y, this.transform.position.z);
            //ActionsWithKikongiSoundsOnly(collision.collider.name);            
        }
    }

    private void ActionsWithKikongiSoundsOnly(string colliderName)
    {
        var soundsKikongi = Kikongi.GetComponentsInChildren<AudioSource>();

        ColliderNote Note = new ColliderNote(soundsKikongi, Notes, colliderName);
        Note.PlayWithoutCollision();
        MovementFishes();
    }


    private void ActionsWithKikongiSounds(Collision collision)
    {
        string colliderName = collision.collider.name;
        var soundsKikongi = Kikongi.GetComponentsInChildren<AudioSource>();

        ColliderNote Note = new ColliderNote(soundsKikongi, Notes, colliderName, collision);
        Note.PlayOnly();
        MovementFishes();
    }


    private void MovementFishes()
    {
        if (fishToAdd== maxPlaysForDolphinAndWhale)
        {
            var posWhale = Helper.FindByTag(Names.WHALEPOSITION).transform.position;
            posWhale = new Vector3(posWhale.x, 0, posWhale.z);
            Helper.FindByTag(Names.WHALEPOSITION).transform.position = posWhale;
            DolphinCircle.SetActive(true);
        }

        AddFish();

        if (!hasMovedFishes.Value)
        {
            MoveFishesToKikongi();
        }
    }

    private void AddFish()
    {
        if (Manager != null)
        {
            if (fishToAdd < 100)
            {
                Manager.AddFish();
            }
        }
    }

    private void MoveFishesToKikongi()
    {
        if (hasMovedFishes.HasValue && !hasMovedFishes.Value)
        {
            var posFishes = GameObject.FindGameObjectsWithTag(Names.FISHESKIKONGI).FirstOrDefault();
            var posKikongi = GameObject.FindGameObjectsWithTag(Names.KIKONGI).FirstOrDefault().transform;

            if (posFishes != null && posKikongi != null)
            {
                posFishes.transform.SetParent(posKikongi);
                posFishes.transform.position = posKikongi.position;
                hasMovedFishes = true;
            }
        }
    }
}
