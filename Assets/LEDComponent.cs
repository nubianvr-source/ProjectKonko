using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VrGrabber;
using TMPro;
public class LEDComponent : MonoBehaviour
{

    //Public Variables

    public Image imageFlip;


    public GameObject LEDMesh;

    public GameObject LEDComp;

    public Sprite positiveFlip;

    public Sprite negativeFlip;

    public Material LEDMatOn;

    public Material LEDMatOff;



    //Default transform position of the light bulb component
    public Transform defaultTransform;

    //Animation Lerp time 
    public float lerpTime = 1f;



    public bool OnGrabbed;

    public VrgGrabbable vrgGrabbableScript;

    public Button flipButton;

    public TextMeshProUGUI LEDDescription;



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
    public LEDTrigger LEDTriggerReference;


    //Light bulb transform reference
    [HideInInspector]
    public Transform LEDTransform;


    //Is Trigger Bool, public but hidden in inspector

    public bool isInTrigger = false;






    // Start is called before the first frame update
    void Start()
    {
        InitComp();
        flipButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        { 
            CheckPositiveBias();
        }
        startLerpPosition = LEDComp.transform.position;
        if (!OnGrabbed)
        { 
          IsLEDInTrigger();
        }
       
    }

    public void UnlockLEDConstraints()
    {
        OnGrabbed = true;
        rigidbodyPhysics.constraints = RigidbodyConstraints.None;
        if (LEDTriggerReference != null)
        {
            LEDTriggerReference.gameObject.SetActive(true);
        }
    }

    private void InitComp()
    {
        defaultLerpPosition = defaultTransform.position;
        rigidbodyPhysics = GetComponent<Rigidbody>();
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        TurnOffLED();
    }

    public void TurnOffLED()
    { 
        isPositiveBias = false;
        LEDMesh.GetComponent<MeshRenderer>().material = LEDMatOff;
    }

    public void FlipPositiveBias()
    {
        isPositiveBias = !isPositiveBias;
    }

    private void CheckPositiveBias()
    {
        if (isPositiveBias)
        {

            imageFlip.GetComponent<Image>().sprite = positiveFlip;
            //LEDIcon.diodeActive = positiveBiasIcon;
            LEDComp.transform.localEulerAngles = new Vector3(0, 0, 0);
            LEDMesh.GetComponent<MeshRenderer>().material = LEDMatOn;

        }
        else
        {
            imageFlip.GetComponent<Image>().sprite = negativeFlip;
            //LEDIcon.diodeActive = negativeBiasIcon;
            LEDComp.transform.localEulerAngles = new Vector3(0, 0, 0);
            LEDMesh.GetComponent<MeshRenderer>().material = LEDMatOff;
        }
    }


    public void OnLEDDropped()
    {
        IsLEDInTrigger();
        OnGrabbed = false;
    }

    private void IsLEDInTrigger()
    {
        if (isInTrigger)
        {
            LEDInTrigger();
        }
        else
        {
            LEDNotInTrigger();
        }
    }


    private void LEDInTrigger()
    {
        LEDComp.transform.position = Vector3.Lerp(startLerpPosition, LEDTransform.position, lerpTime);
        LEDComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        //flipButton.gameObject.SetActive(true);
        //LEDDescription.gameObject.SetActive(false);
        if (LEDTriggerReference != null)
        {
            LEDTriggerReference.gameObject.SetActive(false);
        }

    }

    private void LEDNotInTrigger()
    {
        LEDComp.transform.position = Vector3.Lerp(startLerpPosition, defaultLerpPosition, lerpTime);
        LEDComp.transform.localEulerAngles = new Vector3(0, 0, 0);
        rigidbodyPhysics.velocity = Vector3.zero;
        rigidbodyPhysics.angularVelocity = Vector3.zero;
        rigidbodyPhysics.constraints = RigidbodyConstraints.FreezeAll;
        //flipButton.gameObject.SetActive(false);
        //LEDDescription.gameObject.SetActive(true);
        if (LEDTriggerReference != null)
        {
            LEDTriggerReference.gameObject.SetActive(true);
        }

    }
}
