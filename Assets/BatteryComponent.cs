using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VrGrabber;
using UnityEngine.SceneManagement;

public class BatteryComponent : MonoBehaviour
{
    //Public Variables
    public GameObject batteryComp;

    public GameObject batteryModel;

    public Material normalBatteryMat;

    public Material overheatingBatteryMat;

    //Default transform position of the light bulb component
    public Transform defaultTransform;

    //Animation Lerp time 
    public float lerpTime = 1f;

    //Lerp Time for the animation lentgh of the material trasition of battery exploding
    public float batteryMatAnimLerpTime = 2f;


    public BatteryIconScript batteryIcon;

    public GameObject batteryText;

    public bool OnGrabbed;

    public bool batteryOverheating;

    public VrgGrabbable vrgGrabbableScript;

    public bool turn90 = false;

    

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
    public BatteryTriggerScript batteryTriggerReference;

    //Light bulb transform reference
    [HideInInspector]
    public Transform BatteryTransform;

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
        startLerpPosition = batteryComp.transform.position;
        float lerp = Mathf.PingPong(Time.time, batteryMatAnimLerpTime) / batteryMatAnimLerpTime;

        if (batteryOverheating)
        {
            batteryModel.GetComponent<MeshRenderer>().material.Lerp(normalBatteryMat, overheatingBatteryMat, lerp);
        }
        else 
        {
            batteryModel.GetComponent<MeshRenderer>().material = normalBatteryMat;
        }

        // InEditorVR();
        if (!OnGrabbed)
        {
            IsBatteryInTrigger();
        }
       

        
    }

    public void InEditorVR()
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

    public void unlockBatteryConstraints()
    {
        OnGrabbed = true;
        rigidbodyPhysics.constraints = RigidbodyConstraints.None;
        if (batteryTriggerReference != null)
        {
            batteryTriggerReference.gameObject.SetActive(true);
        }

    }

    public void InitComp()
    {
        defaultLerpPosition = defaultTransform.position;
        rigidbodyPhysics = GetComponent<Rigidbody>();
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        batteryOverheating = false;
        batteryModel.GetComponent<MeshRenderer>().material = normalBatteryMat;
    }

    public void OnBatteryDropped()
    {
        IsBatteryInTrigger();
        OnGrabbed = false;
    }

    private void IsBatteryInTrigger()
    {
        if (isInTrigger)
        {
            BatteryInTrigger();
        }
        else
        {
            BatteryNotInTrigger();
        }
    }

    private void BatteryInTrigger()
    {
        batteryComp.transform.position = Vector3.Lerp(startLerpPosition, BatteryTransform.position, lerpTime);
        if (turn90)
        {
            batteryComp.transform.localEulerAngles = new Vector3(0, 90, 90);
        }

        else
        {
            batteryComp.transform.localEulerAngles = new Vector3(200, 0, 90);
        }
        
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        batteryText.SetActive(false);
        if (batteryTriggerReference != null)
        {
            batteryTriggerReference.gameObject.SetActive(false);
        }
    }

    private void BatteryNotInTrigger()
    {
        batteryComp.transform.position = Vector3.Lerp(startLerpPosition, defaultLerpPosition, lerpTime);
        batteryComp.transform.localEulerAngles = new Vector3(0, -158, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        if (batteryTriggerReference != null)
        {
            batteryTriggerReference.gameObject.SetActive(true);
        }
    }
}
