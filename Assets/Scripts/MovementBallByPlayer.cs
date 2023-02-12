using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBallByPlayer
{
    public float Value { get; set; }
    private float Min;
    private float Max;
    private float pas = 0.001f;

    public MovementBallByPlayer()
    { }

    public MovementBallByPlayer(float value, float max, float min, bool hasMin, bool hasMax)
    {
        Max = max;
        Min = min;
        Value = value;

        if (hasMin)
        {
            SetValueMin();
        }

        if (hasMax)
        {
            SetValueMax();
        }
    }

    private void SetValueMin()
    {
        if (Value > Min)
        {
            Value -= pas;
        }
    }

    private void SetValueMax()
    {
        if (Value < Max)
        {
            Value += pas;
        }
    }
}
