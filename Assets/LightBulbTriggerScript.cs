using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LightBulbTriggerScript : MonoBehaviour
{
    //Light Bulb Game Object Reference
    public GameObject circuitComp;

    //Start Lerp Position of LightBulb Component
    private Vector3 startLerpPosition;

    //End Lerp Position of LightBulb Component
    private Vector3 endLerpPosition;

    //Light bulb transform reference
    public Transform lightBulbTransform;

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

    public LightBulbIcon lightBulbIcon;



    // Start is called before the first frame update
    void Start()
    {
        endLerpPosition = lightBulbTransform.position;
        defaultLerpPosition = defaultTransform.position;

       
    }

    // Update is called once per frame
    void Update()
    {
         startLerpPosition = circuitComp.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LightBulb")
        {
            FindObjectOfType<AudioEngine>().PlayAudio("SuccessTrigger");

            isInTrigger = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LightBulb")
        {
            FindObjectOfType<AudioEngine>().PlayAudio("");
            isInTrigger = false;
            
        }
    }

    public void LightBulbTransform()
    {
        if (isInTrigger)
        {

            circuitComp.transform.position = Vector3.Lerp(startLerpPosition, endLerpPosition, lerpTime);
            // Vector3.MoveTowards(startLerp, endLerpPosition, LerpTime);
            circuitComp.transform.localEulerAngles = new Vector3(0, 0, 0);
            rigidbodyPhysics.velocity = Vector3.zero;
            rigidbodyPhysics.angularVelocity = Vector3.zero;
            //lightBulbIcon.LightBulbIconOn();
            this.gameObject.SetActive(false);
            
            
           
        }

        else {
            circuitComp.transform.position = Vector3.Lerp(startLerpPosition, defaultLerpPosition, lerpTime);
            circuitComp.transform.localEulerAngles = new Vector3(0, 0, 0);
            rigidbodyPhysics.velocity = Vector3.zero;
            rigidbodyPhysics.angularVelocity = Vector3.zero;
        }
    }
}
