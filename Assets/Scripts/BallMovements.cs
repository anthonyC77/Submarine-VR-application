using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class BallMovements : MonoBehaviour
{
    public GameObject PositionPlayer;
    public float StartMiddle = 7;
    private float Start;
    private float End;
    
    bool switchDirection = false;
    bool start = true;
    bool end = false;

    [Range(MinSpeedConst, MaxSpeedConst)]
    public float Speed;
    public float MinSpeed = MinSpeedConst;
    public float MaxSpeed = MaxSpeedConst;
    public const float MinSpeedConst = 0f;
    public const float MaxSpeedConst = 2f;

    [Range(MinHSizeBallConst, MaxSizeBallConst)]
    public float SizeBall = 0.05f; 
    public float MinSizeBall = MinHSizeBallConst;
    public float MaxSizeBall = MaxSizeBallConst;
    public const float MinHSizeBallConst = 0.01f;
    public const float MaxSizeBallConst = 0.1f;
    
    [Range(MinHeightConst, MaxHeightConst)]
    public float Height = 0.4f;
    public float MinHeight = MinHeightConst;
    public float MaxHeight = MaxHeightConst;
    public const float MinHeightConst = 0.1f;
    public const float MaxHeightConst = 0.7f;

    [Range(MinDepthConst, MaxDepthConst)]
    public float Depth;
    public float MinDepth = MinDepthConst;
    public float MaxDepth = MaxDepthConst;
    public const float MinDepthConst = 0.1f;
    public const float MaxDepthConst = 1f;

    [Range(MinDistanceFromMiddleConst, MaxDistanceFromMiddleConst)]
    public float DistanceFromMiddle = 0.5f;
    public float MinDistanceFromMiddle = MinDistanceFromMiddleConst;
    public float MaxDistanceFromMiddle = MaxDistanceFromMiddleConst;
    public const float MinDistanceFromMiddleConst = 0.2f;
    public const float MaxDistanceFromMiddleConst = 1;
    
    public AudioSource SoundToPlay;
    bool PlayingSound = false;
    private float pas = 0.001f;
    DateTime? StartPlay;
    DateTime? TriggerPlay;

    public void Awake()
    {
        this.transform.localScale = new Vector3(SizeBall, SizeBall, SizeBall);
        StartCoroutine(time());
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(300);
        SceneManager.LoadScene(Names.NEPTUNE);
    }


    private void ResetPosition()
    {
        Speed = 0f;
        Height = 0.5f;
        Depth = 0.6f;
        SizeBall = 0.07f;
        Depth = 0.9f;
        this.transform.position = new Vector3(
            7, this.transform.position.y, this.transform.position.z);
    }

    private enum Direction
    {
        UP,
        DOWN,
        DEPTH,
        UNDEPTH,
        SMALLER,
        BIGGER,
        QUICKER,
        SLOWER,
        RESET,
        BEGIN,
        NONE,
        TRIGGERPLAY,
        FARFROMMIDDLE,
        CLOTHFROMMIDDLE,
    }

    private void PlaySounds()
    {
        StartCoroutine(PlaySound());
    }

    private void PlaySoundAndrecord()
    {
        if (!TriggerPlay.HasValue && StartPlay.HasValue)
        {
            // error
            // todo record in XML datas with timePlay, timeRespond, OK or KO

        }

        PlayingSound = true;
        SoundToPlay.PlayOneShot(SoundToPlay.clip);
        StartPlay = DateTime.Now;
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(30f);
        for (int i1 = 0; i1 < 9; i1++)
        {
            yield return new WaitForSeconds(RandomValue());
            SoundToPlay.PlayOneShot(SoundToPlay.clip);
        }

        for (int i = 0; i < 5; i++)
        {
            if (PlayingSound)
            {
                yield return new WaitForSeconds(30f);
                for (int i1 = 0; i1 < 80; i1++)
                {
                    yield return new WaitForSeconds(RandomValue());
                    PlaySoundAndrecord();
                }                
            }
            else
                break;            
        }
    }

    private float RandomValue()
    {
        var rand = UnityEngine.Random.Range(0.9f, 1.5f);
        return rand;
    }

    private void SetDirectionOculus()
    {
        Direction dir = Direction.NONE;

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
            dir = Direction.UP;

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
            dir = Direction.DOWN;

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
            dir = Direction.SMALLER;

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
            dir = Direction.BIGGER;

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
            dir = Direction.DEPTH;

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            dir = Direction.UNDEPTH;

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
            dir = Direction.QUICKER;

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
            dir = Direction.SLOWER;

        if (OVRInput.Get(OVRInput.Button.One))
            dir = Direction.RESET;

        if (OVRInput.Get(OVRInput.Button.Two))
            dir = Direction.BEGIN;

        if (OVRInput.Get(OVRInput.Button.Three))
            dir = Direction.TRIGGERPLAY;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            dir = Direction.CLOTHFROMMIDDLE;

        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            dir = Direction.FARFROMMIDDLE;

        SetParam(dir);
    }

    private void SetDirection()
    {
        Direction dir = Direction.NONE;

        if (Input.GetKey(KeyCode.UpArrow))
            dir = Direction.UP;

        if (Input.GetKey(KeyCode.DownArrow))
            dir = Direction.DOWN;

        if (Input.GetKey(KeyCode.LeftArrow))
            dir = Direction.SMALLER;

        if (Input.GetKey(KeyCode.RightArrow))
            dir = Direction.BIGGER;

        if (Input.GetKey(KeyCode.Z))
            dir = Direction.DEPTH;

        if (Input.GetKey(KeyCode.S))
            dir = Direction.UNDEPTH;

        if (Input.GetKey(KeyCode.D))
            dir = Direction.QUICKER;

        if (Input.GetKey(KeyCode.Q))
            dir = Direction.SLOWER;

        if (Input.GetKey(KeyCode.R))
            dir = Direction.RESET;

        if (Input.GetKey(KeyCode.B))
            dir = Direction.BEGIN;

        if (Input.GetKey(KeyCode.Keypad1))
            dir = Direction.CLOTHFROMMIDDLE;

        if (Input.GetKey(KeyCode.B))
            dir = Direction.BEGIN;

        if (Input.GetKey(KeyCode.Keypad3))
            dir = Direction.FARFROMMIDDLE;

        SetParam(dir);
    }    

    private void SetParam(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
            case Direction.DOWN:
                var movHeight = new MovementBallByPlayer(Height, MaxHeight, MinHeight, direction == Direction.DOWN, direction == Direction.UP);
                Height = movHeight.Value;
                break;
            case Direction.DEPTH:
            case Direction.UNDEPTH:
                var movDepth = new MovementBallByPlayer(Depth, MaxDepth, MinDepth, direction == Direction.UNDEPTH, direction == Direction.DEPTH);
                Depth = movDepth.Value;
                break;
            case Direction.BIGGER:
            case Direction.SMALLER:
                var movSizeBall = new MovementBallByPlayer(SizeBall, MaxSizeBall, MinSizeBall, direction == Direction.SMALLER, direction == Direction.BIGGER);
                SizeBall = movSizeBall.Value;
                break;
            case Direction.QUICKER:
            case Direction.SLOWER:
                var movSpeed = new MovementBallByPlayer(Speed, MaxSpeed, MinSpeed, direction == Direction.SLOWER, direction == Direction.QUICKER);
                Speed = movSpeed.Value;
                break;

            case Direction.RESET:
                ResetPosition();
                break;

            case Direction.BEGIN:
                ResetPosition();
                Speed = MaxSpeedConst;
                PlaySounds();
                break;

            case Direction.TRIGGERPLAY:
                TriggerPlayButton();
                break;

            case Direction.CLOTHFROMMIDDLE:
            case Direction.FARFROMMIDDLE:
                var movFromMiddle = new MovementBallByPlayer(DistanceFromMiddle, MaxDistanceFromMiddle, MinDistanceFromMiddle, 
                    direction == Direction.CLOTHFROMMIDDLE, direction == Direction.FARFROMMIDDLE);
                DistanceFromMiddle = movFromMiddle.Value;
                break;

            default:
                break;
        }
    }

    private void TriggerPlayButton()
    {
        TriggerPlay = DateTime.Now;
        var elpased = TriggerPlay - StartPlay;
        PlayingSound = false;
    }

    private void SetSizeAndPose()
    {
        this.transform.localScale = new Vector3(SizeBall, SizeBall, SizeBall);
        var x = this.transform.position.x;
        var y = PositionPlayer.transform.position.y + Height;
        var z = PositionPlayer.transform.position.z - Depth;
        this.transform.position = new Vector3(x, y, z);
        Start = StartMiddle + DistanceFromMiddle;
        End = StartMiddle - DistanceFromMiddle;
    }
        
    // Update is called once per frame
    void Update()
    {
        //SetDirection();
        SetDirectionOculus();
        SetSizeAndPose();

        float nextMov = 0;

        if (start)
        {
            switchDirection = this.transform.position.x >= Start;
            if (switchDirection)
            {
                start = false;
                end = true;
            }
        }

        if (end)
        {
            switchDirection = this.transform.position.x <= End;
            if (switchDirection)
            {
                start = true;
                end = false;
            }
        }
               
        if (start)
            nextMov = Start;
        else
            nextMov = End;

        this.transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(nextMov, this.transform.position.y, this.transform.position.z), Speed * Time.deltaTime);

    }
}
