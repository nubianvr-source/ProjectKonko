using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VrGrabber;

public class LightBulbComponent : MonoBehaviour
{

    //Public Variables

    public GameObject lightBulbComp;

    public GameObject lightBulbMesh;

    public Material LightOffMat;

    public Material LightOnMat;

    public bool GrabbedComp;

    public VrgGrabbable vrgGrabbableScript;

    //Default transform position of the light bulb component 
    public Transform defaultTransform;

    //Animation Lerp time 
    public float lerpTime = 1f;    

    public LightBulbIcon lightBulbIcon;

    public bool isInSeries;




    //Private Variables

    //Default Lerp postion of the light bulb as gotten from the default Transform variable on Start
    private Vector3 defaultLerpPosition;

    //Start Lerp Position of LightBulb Component
    private Vector3 startLerpPosition;




    //Hidden Inpector Variables
    [HideInInspector]
    public LightBulbTriggerScript lightBulbTriggerReference;

    //Controi Physics Component of the Light Bulb Component
    [HideInInspector]
    public Rigidbody rigidbodyPhysics;

    //Light bulb Trigger transform reference
    [HideInInspector]
    public Transform lightBulbTransform;

  
    public bool isInTrigger = false;








    // Start is called before the first frame update
    void Start()
    {
        lightOff();
        defaultLerpPosition = defaultTransform.position;
        rigidbodyPhysics = GetComponent<Rigidbody>();
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {

        startLerpPosition = lightBulbComp.transform.position;

        // InEditorVR();
        if (!GrabbedComp)
        { 
          IsLightBulbInTrigger();
        }

       
         
    }

    public void InEditorVR()
    { 
        if (GrabbedComp == true)
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

    public void unlockLightBulbConstraints()
    {
        GrabbedComp = true;
        rigidbodyPhysics.constraints = RigidbodyConstraints.None;
        if (lightBulbTriggerReference != null)
        {
            lightBulbTriggerReference.gameObject.SetActive(true);
        }

    }

    public void lightOn() 
    {
        lightBulbMesh.GetComponent<MeshRenderer>().material = LightOnMat;
        lightBulbIcon.LightBulbIconOn();
    }

    public void lightOff()
    {
        lightBulbMesh.GetComponent<MeshRenderer>().material = LightOffMat;
        lightBulbIcon.LightBulbIconOff();
    }

    public void OnLlightBulbDropped()
    {
        IsLightBulbInTrigger();
        GrabbedComp = false;
    }

    private void IsLightBulbInTrigger()
    {
        if (isInTrigger)
        {
            LightBulbIsInTrigger();
        }
        else
        {
            LightBulbIsNotInTirigger();
        }
    }

    private void LightBulbIsInTrigger()
    {
        lightBulbComp.transform.position = Vector3.Lerp(startLerpPosition, lightBulbTransform.position, lerpTime);
        lightBulbComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        if (lightBulbTriggerReference != null)
        { 
         lightBulbTriggerReference.gameObject.SetActive(false);
        }
    }

    private void LightBulbIsNotInTirigger()
    {
        lightBulbComp.transform.position = Vector3.Lerp(startLerpPosition, defaultLerpPosition, lerpTime);
        lightBulbComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        if (lightBulbTriggerReference != null)
        {
            lightBulbTriggerReference.gameObject.SetActive(true);
        }
        
    }
}
