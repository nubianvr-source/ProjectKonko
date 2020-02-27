﻿using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
    
    public UnityEvent displaySentencesFinished = new UnityEvent();
}
