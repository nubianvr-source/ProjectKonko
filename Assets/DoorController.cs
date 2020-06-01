using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    private void Start()
    {
        GameEventManager.current.OnDoorwayTriggerEnter += DoorwayOpen;
        GameEventManager.current.OnDoorwayTriggerExit += DoorwayClose;
    }


    private void OnDisable()
    {
        GameEventManager.current.OnDoorwayTriggerEnter -= DoorwayOpen;
        GameEventManager.current.OnDoorwayTriggerExit -= DoorwayClose;
    }

    private void DoorwayOpen(int id)
    {
        if(id == this.id)
        LeanTween.moveLocalY(gameObject, -0.56f, 1f).setEaseInOutQuad();
    }

    private void DoorwayClose(int id)
    {
        if(id == this.id)
        LeanTween.moveLocalY(gameObject, -1.577976f, 1f).setEaseInOutQuad();
    }
}
