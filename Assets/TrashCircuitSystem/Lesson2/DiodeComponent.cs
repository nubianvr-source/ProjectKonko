using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VrGrabber;
using TMPro;

public class DiodeComponent : MonoBehaviour
{
    //Public Variables

    public DiodeIconScript diodeIcon;

    public Sprite positiveBiasIcon;

    public Sprite negativeBiasIcon;

    public Image imageFlip;


    public GameObject diodeMesh;

    public GameObject diodeComp;

    public Sprite positiveFlip;

    public Sprite negativeFlip;


    //Default transform position of the light bulb component
    public Transform defaultTransform;

    //Animation Lerp time 
    public float lerpTime = 1f;

    //public ResistorIconScript batteryIcon;



    public bool OnGrabbed;

    public VrgGrabbable vrgGrabbableScript;

    public Button flipButton;

    public TextMeshProUGUI diodeDescription;



    //Private Variables

    //Start Lerp Position of LightBulb Component
    private Vector3 startLerpPosition;

    //Default Lerp postion of the light bulb as gotten from the default Transform variable on Start
    private Vector3 defaultLerpPosition;

    //Hidden Variables

    [HideInInspector]
    public bool isPositiveBias;

    [HideInInspector]
    public Rigidbody rigidbodyPhysics;

    [HideInInspector]
    public DiodeTriggerScript diodeTriggerReference;


    //Light bulb transform reference
    [HideInInspector]
    public Transform diodeTransform;


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
        CheckPositiveBias();
        startLerpPosition = diodeComp.transform.position;
        //InEditorVR();
        if (!OnGrabbed)
        {
            IsDiodeInTrigger();
            
        }
    }

    /*
    public void flipSwitchUI()
    {
        wireScript.setCircuitInactive();
        print("Button has been clicked"); 
        isPositiveBias = !isPositiveBias;
        if (isPositiveBias)

        {
            imageFlip.GetComponent<Image>().sprite = positiveFlip;
            diodeIcon.diodeActive = positiveBiasIcon;
            diodeMesh.transform.localEulerAngles = new Vector3(0, 0, -90);
           // 
            wireScript.setCircuitActive();

        }

        else 
        {
            imageFlip.GetComponent<Image>().sprite = negativeFlip;
            diodeIcon.diodeActive = negativeBiasIcon;
            diodeMesh.transform.localEulerAngles = new Vector3(0, 0, 90);
           // wireScript.setCircuitInactive();
            wireScript.setCircuitActive();
        }
    }
    */


    private void InitComp()
    {
        defaultLerpPosition = defaultTransform.position;
        rigidbodyPhysics = GetComponent<Rigidbody>();
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        diodeIcon.diodeActive = positiveBiasIcon;
        isPositiveBias = true;
        flipButton.gameObject.SetActive(false);

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

    public void UnlockDiodeConstraints()

    {
        OnGrabbed = true;
        rigidbodyPhysics.constraints = RigidbodyConstraints.None;
        if (diodeTriggerReference != null)
        {
            diodeTriggerReference.gameObject.SetActive(true);
        }

    }


    public void OnDiodeDropped()
    {
        OnGrabbed = false;

        IsDiodeInTrigger();
    }

    private void IsDiodeInTrigger()
    {
        if (isInTrigger)
        {
           DiodeInTrigger();
        }
        else
        {
            DiodeNotInTrigger();
        }
    }


    private void DiodeInTrigger()
    {
        diodeComp.transform.position = Vector3.Lerp(startLerpPosition, diodeTransform.position, lerpTime);
        diodeComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        flipButton.gameObject.SetActive(true);
        diodeDescription.gameObject.SetActive(false);
        if (diodeTriggerReference != null)
        {
            diodeTriggerReference.gameObject.SetActive(false);
        }
    }

    private void DiodeNotInTrigger()
    {
        diodeComp.transform.position = Vector3.Lerp(startLerpPosition, defaultLerpPosition, lerpTime);
        diodeComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        flipButton.gameObject.SetActive(false);
        diodeDescription.gameObject.SetActive(true);
        if (diodeTriggerReference != null)
        {
            diodeTriggerReference.gameObject.SetActive(true);
        }
    }


    public void FlipPositiveBias()
    { 
       isPositiveBias = !isPositiveBias;
    }

    public void CheckPositiveBias()
    {
      
        if (isPositiveBias)
        {

            imageFlip.GetComponent<Image>().sprite = positiveFlip;
            diodeIcon.diodeActive = positiveBiasIcon;
            diodeMesh.transform.localEulerAngles = new Vector3(0, 0, -90);

        }
        else
        {
            imageFlip.GetComponent<Image>().sprite = negativeFlip;
            diodeIcon.diodeActive = negativeBiasIcon;
            diodeMesh.transform.localEulerAngles = new Vector3(0, 0, 90);

        }
    
    }

    
}
