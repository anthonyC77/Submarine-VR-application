using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchGarbages : MonoBehaviour
{
    public GameObject OverCan;
    public int MaxCount;
    private int trashCount = 0;
    public List<GameObject> ObjectsToAwake;
    public AudioSource PickeUpDolphin;
    public ParticleSystem CatchGarbage;

    private void Awake()
    {
        CatchGarbage.Stop();
    }

    IEnumerator Catches()
    {
        CatchGarbage.Play();
        yield return new WaitForSeconds(0.1f);
        CatchGarbage.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(Names.GARBAGE))
        {
            if (OverCan != null)
            {
                collision.gameObject.transform.position = OverCan.transform.position;
                PickeUpDolphin.PlayOneShot(PickeUpDolphin.clip);
                StartCoroutine(Catches());               
                trashCount++;
            }            
        }

        if (trashCount == MaxCount)
        {
            // kikongi and 
            foreach (var ObjectToAwake in ObjectsToAwake)
            {
                ObjectToAwake.SetActive(true);
            }

            Helper.FindByTag(Names.TRAUMA).SetActive(false);
        }
    }
}
