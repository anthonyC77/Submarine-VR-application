using Assets.Scripts.Neptune;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaillocheKikongiTwoFaces : MonoBehaviour
{
    public GameObject door; 
    List<GameObject> Notes = new List<GameObject>();
    Recorder recorder;
    ColliderNote Note;
    public GameObject Kikongi;
    public int SwitchToFractal = 10;
    public float AugmentationPas = 0.25f;

    private void Awake()
    {
        door = GameObject.FindGameObjectWithTag(Names.DOOROFPERCEPTION);
        door.SetActive(false);
        Notes = GameObject.FindGameObjectsWithTag(Names.NOTES).ToList();
        //recorder = new Recorder();
    }

    bool hasMovedFishes = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Names.KIKONGI )
        {
            // recul and sound
        }
        else if(collision.gameObject.tag.StartsWith(Names.NOTE))
        {
            ActionsWithKikongiSounds(collision);
            MoveFishesToKikongi();
        }       
    }

    private void MoveFishesToKikongi()
    {
        if (!hasMovedFishes)
        {
            var posFishes = GameObject.FindGameObjectsWithTag(Names.FISHESKIKONGI).FirstOrDefault();
            var posKikongi = GameObject.FindGameObjectsWithTag(Names.KIKONGI).FirstOrDefault().transform;
            posFishes.transform.SetParent(posKikongi);
            posFishes.transform.position = posKikongi.position;
            hasMovedFishes = true;
        }
    }

    private void ActionsWithKikongiSounds(Collision collision)
    {
        string colliderName = collision.collider.name;
        var soundsKikongi = Kikongi.GetComponentsInChildren<AudioSource>();       
        
        Note = new ColliderNote(soundsKikongi, Notes, colliderName, collision);
        SetColorFish(colliderName);

        Note.PlayOnly();
        Kikongi.GetComponent<Light>().intensity += AugmentationPas;
        Kikongi.GetComponent<Light>().range += AugmentationPas;

        if (Kikongi.GetComponent<Light>().intensity > SwitchToFractal)
        {
            door.SetActive(true);           
        }
    }

    private void SetColorFish(string name)
    {
        var note = Helper.GetEnumValueByName<eNotes2Faces>(name);
        var fish = GameObject.Find(Names.FISHKIKONGI + (int)note + "(Clone)") ;
        if (fish != null)
        {
            var light = fish.GetComponent<Light>();
            light.enabled = true;
            light.color = GetColor(note);
        }
    }

    private Color GetColor(eNotes2Faces note)
    {
        var color = new Color();
        switch (note)
        {
            case eNotes2Faces.Note1_A:
                color = new Color(67, 115, 115);
                break;
            case eNotes2Faces.Note1_G:
                color = new Color(195, 133, 41);
                break;
            case eNotes2Faces.Note1_B:
                color = new Color(180, 158, 45);
                break;
            case eNotes2Faces.Note1_D:
                color = new Color(212, 226, 57);
                break;
            case eNotes2Faces.Note1_C:
                color = new Color(81, 160, 161);
                break;
            case eNotes2Faces.Note1_C2:
                color = new Color(203, 113, 49);
                break;
            case eNotes2Faces.Note1_A2:
                color = new Color(59, 107, 157);
                break;
            case eNotes2Faces.Note1_E:
                color = new Color(162, 61,50);
                break;
            case eNotes2Faces.Note2_A:
                color = new Color(80, 154, 146);
                break;
            case eNotes2Faces.Note2_G:
                color = new Color(101, 106, 41);
                break;
            case eNotes2Faces.Note2_B:
                color = new Color(111, 215, 173);
                break;
            case eNotes2Faces.Note2_D:
                color = new Color(198, 157, 77);
                break;
            case eNotes2Faces.Note2_C:
                color = new Color(117, 174, 157);
                break;
            case eNotes2Faces.Note2_C2:
                color = new Color(41, 161, 173);
                break;
            case eNotes2Faces.Note2_A2:
                color = new Color(45, 154, 166);
                break;
            case eNotes2Faces.Note2_E:
                color = new Color(234, 255, 0);
                break;
            default:
                break;
        }

        return color;
    }
}
