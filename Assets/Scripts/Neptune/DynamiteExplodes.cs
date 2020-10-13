using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplodes : MonoBehaviour
{
    public GameObject BeforeExplosion;
    public GameObject Explosion;
    public GameObject AfterExplosion;
    public float TimeBeforeExplosion = 10;
    public AudioSource ExplosionSound;

    private void Start()
    {
        ApplySetActive(Explosion, false);
        ApplySetActive(AfterExplosion, false);
        StartCoroutine(ExplodingTime());
    }

    IEnumerator ExplodingTime()
    {
        yield return new WaitForSeconds(TimeBeforeExplosion);
        ApplySetActive(Explosion, true);
        if (ExplosionSound != null)
        {
            ExplosionSound.PlayOneShot(ExplosionSound.clip);
        }        
        yield return new WaitForSeconds(0.1f);
        ApplySetActive(BeforeExplosion, false);
        yield return new WaitForSeconds(0.2f);
        ApplySetActive(Explosion, false);
        ApplySetActive(AfterExplosion, true);
    }

    private void ApplySetActive(GameObject explode, bool setactive)
    {
        if (explode != null)
        {
            explode.SetActive(setactive);
        }
    }
}
