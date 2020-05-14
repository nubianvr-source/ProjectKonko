using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryTriggerScript : MonoBehaviour
{
    //Light Bulb Game Object Reference
    public GameObject circuitComp;

    //Start Lerp Position of LightBulb Component
    private Vector3 startLerpPosition;

    //End Lerp Position of LightBulb Component
    private Vector3 endLerpPosition;

    //Light bulb transform reference
    public Transform BatteryTransform;

    //Animation Lerp time 
    public float lerpTime = 1f;

    //Controi Physics Component of the Light Bulb Component
    public Rigidbody rigidbodyPhysics;

    //Is Trigger Bool, public but hidden in inspector
    [HideInInspector]
    public bool isInTrigger = false;

    //Default transform position of the light bulb component
    public Transform defaultTransform;

    //Default Lerp postion of the light bulb as gotten from the default Transform variable on Start
    private Vector3 defaultLerpPosition;

    public BatteryIconScript batteryIcon;
    public GameObject batteryText;

 


    // Start is called before the first frame update
    void Start()
    {
        endLerpPosition = BatteryTransform.position;
        defaultLerpPosition = defaultTransform.position;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
       
        //isInTrigger = true;
        //BatteryBulbTransform();
    }

    // Update is called once per frame
    void Update()
    {
        startLerpPosition = circuitComp.transform.position;
        
    }


    public void unlockBatteryConstraints()
    {
        rigidbodyPhysics.constraints = RigidbodyConstraints.None;

    }

    public void SetTriggerActive()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Battery")
        {
           FindObjectOfType<SoundManager>().Play("SuccessTrigger");
            var battery = other.GetComponent<BatteryComponent>();
            battery.batteryTriggerReference = this;
            battery.BatteryTransform = BatteryTransform;
            battery.isInTrigger = true;
            isInTrigger = battery.isInTrigger;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Battery")
        {
            FindObjectOfType<SoundManager>().Play("");
            var battery = other.GetComponent<BatteryComponent>();
            battery.batteryTriggerReference = this;
            battery.isInTrigger = false;
            isInTrigger = battery.isInTrigger;
        }
    }

    public void BatteryBulbTransform()
    {
        if (isInTrigger)
        {

            circuitComp.transform.position = Vector3.Lerp(startLerpPosition, endLerpPosition, lerpTime);
            // Vector3.MoveTowards(startLerp, endLerpPosition, LerpTime);
            circuitComp.transform.localEulerAngles = new Vector3(200, 0, 90);
            rigidbodyPhysics.velocity = Vector3.zero;
            rigidbodyPhysics.angularVelocity = Vector3.zero;
            rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
            batteryText.SetActive(false);
            this.gameObject.SetActive(false);
        }

        else
        {
            circuitComp.transform.position = Vector3.Lerp(startLerpPosition, defaultLerpPosition, lerpTime);
            circuitComp.transform.localEulerAngles = new Vector3(0, -158, 0);
            rigidbodyPhysics.velocity = Vector3.zero;
            rigidbodyPhysics.angularVelocity = Vector3.zero;
            rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
            this.gameObject.SetActive(false);
        }
    }
}
