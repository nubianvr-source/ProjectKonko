using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> OnDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if (OnDoorwayTriggerEnter != null)
        {
            OnDoorwayTriggerEnter(id);
        }
    }

    public event Action<int> OnDoorwayTriggerExit;
    public void DoorwayTriggerExit(int id)
    {
        if (OnDoorwayTriggerExit != null)
        {
            OnDoorwayTriggerExit(id);
        }
    }

}
