using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VrGrabber;

public class ResistorComponent : MonoBehaviour
{

    //Public Variables
    public GameObject resistorComp;

    //Default transform position of the light bulb component
    public Transform defaultTransform;

    //Animation Lerp time 
    public float lerpTime = 1f;

  //public ResistorIconScript batteryIcon;

    public GameObject resistorText;

    public bool OnGrabbed;

    public float resistanceFloat;

    public VrgGrabbable vrgGrabbableScript;


    //Private Variables

    //Start Lerp Position of LightBulb Component
    private Vector3 startLerpPosition;

    //Default Lerp postion of the light bulb as gotten from the default Transform variable on Start
    private Vector3 defaultLerpPosition;




    //Hidden Variables

    //Controi Physics Component of the Light Bulb Component
    [HideInInspector]
    public Rigidbody rigidbodyPhysics;

    [HideInInspector]
    public ResistorTrigger resistorTriggerReference;


    //Light bulb transform reference
    [HideInInspector]
    public Transform resistorTransform;


    //Is Trigger Bool, public but hidden in inspector

    public bool isInTrigger = false;





    // Start is called before the first frame update
    void Start()
    {
        InitComp();
    }

    // Update is called once per frame
    void Update()
    {
        startLerpPosition = resistorComp.transform.position;
        // InEditorVR();
        if (!OnGrabbed)
        { 
            IsResistorInTrigger();
        }
       
    }


    private void InitComp()
    {
        defaultLerpPosition = defaultTransform.position;
        rigidbodyPhysics = GetComponent<Rigidbody>();
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
       
    }

   private void InEditorVR()
    {
        //This if statement block is meant for debugging withing the editor 

        if (OnGrabbed == true)
        {
            if (vrgGrabbableScript.onGrabbed != null)
            {
                vrgGrabbableScript.onGrabbed.Invoke();
            }

        }
        else
        {
            if (vrgGrabbableScript.onReleased != null)
            {
                vrgGrabbableScript.onReleased.Invoke();
            }
        }
    }


    public void UnlockResistorConstraints()
    {
        OnGrabbed = true;
        rigidbodyPhysics.constraints = RigidbodyConstraints.None;
        if (resistorTriggerReference != null)
        {
            resistorTriggerReference.gameObject.SetActive(true);
        }

    }

    public void OnResistorDropped()
    {
        IsResistorInTrigger();
        OnGrabbed = false;
    }

    private void IsResistorInTrigger()
    {
        if (isInTrigger)
        {
            ResistorInTrigger();
        }
        else
        {
            ResistorNotInTrigger();
        }
    }


    private void ResistorInTrigger()
    {
        resistorComp.transform.position = Vector3.Lerp(startLerpPosition, resistorTransform.position, lerpTime);
        resistorComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        resistorText.SetActive(false);
        if (resistorTriggerReference != null)
        {
            resistorTriggerReference.gameObject.SetActive(false);
        }
    }

    private void ResistorNotInTrigger()
    {
        resistorComp.transform.position = Vector3.Lerp(startLerpPosition, defaultLerpPosition, lerpTime);
        resistorComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        if (resistorTriggerReference != null)
        {
            resistorTriggerReference.gameObject.SetActive(true);
        }
    }

}
