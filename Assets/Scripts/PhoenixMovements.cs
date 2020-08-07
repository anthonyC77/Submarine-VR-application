using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PhoenixMovements : MonoBehaviour
{
    public AudioSource TakeSword;
    public GameObject Phoenix;
    public GameObject Sword;
    public AudioSource PhoenixSound;
    public float Speed = 1;
    bool firstSound = true;
    bool secondSound = true;
    bool PhoenixGoesUp = false;
    public GameObject Player;
    bool startFly = false;
    bool playable = true;
    bool playablePhoenix = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x < 2.5f)
        {
            startFly = true;
        }

        if (startFly)
        {
            PhoenixAndCubeUp();
            PhoenixUp();
        }
    }

    void PlayPhoenix()
    {
        PhoenixSound.PlayOneShot(PhoenixSound.clip, 0.1f);    
    }

    void PhoenixAndCubeUp()
    {
        var pos = this.transform.position;

        if (pos.y >1.4f)
        {
            this.transform.Translate(new Vector3(0, -(Speed * Time.deltaTime), 0));

            if (firstSound)
            {
                PlayPhoenix();
                firstSound = false;
            }
        }
        else if (Sword.transform.position.y > 0)
        {
            if (secondSound)
            {
                PlayPhoenix();
                secondSound = false;
            }
            PhoenixGoesUp = true;
        }
        else
        {
            if (playable)
            {
                TakeSword.PlayOneShot(TakeSword.clip);
                playable = false;
            }
        }

        WingsFly(true);
    }

    private void WingsFly(bool fly)
    {
        Phoenix.GetComponent<Animator>().SetBool(TagNames.FLY, fly);
    }

    private float timeCount = 0.0f;

    void RotateUntil(GameObject wing, Quaternion from, Quaternion to)
    {
        wing.transform.rotation = Quaternion.Slerp(from, to, timeCount);
        timeCount = timeCount + Time.deltaTime;
    }

    void PhoenixUp()
    {
        if (PhoenixGoesUp)
        {
            if (Phoenix.transform.position.y < 15)
            {
                Phoenix.transform.Translate(new Vector3(0, +(Speed * Time.deltaTime), 0));
            }
            else
            {
                if (playablePhoenix)
                {
                    PlayPhoenix();
                    playablePhoenix = false;
                }
                
                WingsFly(false);
            }
        }
    }
}
