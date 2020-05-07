using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapacitorWire : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;

    public GameObject wireModel;


    public WireIconScript wireIcon;

    [Header("Baterry Properties")]
    public BatteryTriggerInitialized batteryTrigger;
    private bool isBatteryInTrigger;

    [Header("Light Bulb Properties")]
    public LLightBulbTriggerBulbInitiallized lightBulbTrigger;
    private bool isLightBulbInTrigger;

    [Header("Diode Properties")]
    //public DiodeObjectScript diodeObject;
    public CapacitorTrigger capacitor;
    private bool isDiodePositiveBias;

    [Header("Switch Properties")]
    public SwitchIconScript switchIcon;
    public TextMeshProUGUI infoTextBox;
    public SwitchComponent switchComponentRef;

    [Header("Other")]
    public LightBulbComponent bulbComponent;
    public GameObject continueBtn;

    public float capacitorWaitTime = 3f;




    [HideInInspector]
    public bool isCurrentRunning;



    // Start is called before the first frame update
    void Start()
    {
        setCircuitInactive();
        continueBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isBatteryInTrigger = batteryTrigger.isInTrigger;
        isLightBulbInTrigger = lightBulbTrigger.isInTrigger;
       // isDiodePositiveBias = diodeObject.isPositiveBias;

    }

    public void setCircuitActive()
    {
        if (isBatteryInTrigger)
        {
            if (isLightBulbInTrigger)
            {

                if (capacitor.isInTrigger)
                {
                    StartCoroutine(CapacitorTurnOnWait());

                    isCurrentRunning = true;
                    infoTextBox.text = "Wait for it";
                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                    lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                    batteryTrigger.batteryIcon.BatteryPowerActive();
                    wireIcon.WireActiveFunc();
                    continueBtn.SetActive(true);
                    switchIcon.SwitchOn();

                }


                else
                {
                    isCurrentRunning = true;
                    infoTextBox.text = "Try placing the capacitor into the circuit";
                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                    lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                    batteryTrigger.batteryIcon.BatteryPowerActive();
                    wireIcon.WireActiveFunc();
                    //continueBtn.SetActive(true);
                    switchIcon.SwitchOn();
                    bulbComponent.lightOn();

                }

            }

            else
            {
                //Play a sound prompt or indicate error for no Light Bulb here
                infoTextBox.text = "Please place the LightBulb in the circuit";
            }

        }
        else
        {
            //Play a sound prompt or indicate error for no battery in circuit here
            infoTextBox.text = "Please place the battery in the circuit to continue";

        }


    }

    IEnumerator CapacitorTurnOnWait()

    {
        yield return new WaitForSeconds(capacitorWaitTime);
        bulbComponent.lightOn();
        switch (capacitorWaitTime)
        {
            case 2:
                infoTextBox.text = "The light is on, now lets try one with a bigger capacity, click the continue button to proceed";
                break;
            case 5:
                infoTextBox.text = "The light is on, now lets try one with an even bigger capacity, click the continue button to proceed";
                break;
            case 7:
                infoTextBox.text = "Great we tried all capacitors with variant capacities and saw how they all worked, now let's continue with the lesson";
                break;
            default:
                infoTextBox.text = "The light is on";
                break;
        }
        

    }
    IEnumerator CapacitorTurnOffWait()

    {
        yield return new WaitForSeconds(capacitorWaitTime);
        bulbComponent.lightOff();
        wireModel.GetComponent<MeshRenderer>().material = wireInactive;
        wireIcon.WireInactiveFunc();
        lightBulbTrigger.lightBulbIcon.LightBulbIconOff();


    }
    public void setCircuitInactive()
    {
        isCurrentRunning = false;
       
        batteryTrigger.batteryIcon.BatteryPowerInactive();
        if (capacitor.isInTrigger)
        {
            switchIcon.SwitchOff();
            StartCoroutine(CapacitorTurnOffWait());
        }
        else 
        {
            bulbComponent.lightOff();
            wireModel.GetComponent<MeshRenderer>().material = wireInactive;
            wireIcon.WireInactiveFunc();
            lightBulbTrigger.lightBulbIcon.LightBulbIconOff();
        }


    }

    public void toggleChanged()
    {

        if (switchComponentRef.switchBool)
        {

            setCircuitActive();

        }
        else
        {

            setCircuitInactive();
        }

    }
}
