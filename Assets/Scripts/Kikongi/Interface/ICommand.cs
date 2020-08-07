using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    DateTime DatePlay { get; set; }
}   