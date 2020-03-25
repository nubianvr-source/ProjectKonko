using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapacitorWire : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;

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
                    GetComponent<LineRenderer>().material = LiveWire;
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
                    GetComponent<LineRenderer>().material = LiveWire;
                    lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                    batteryTrigger.batteryIcon.BatteryPowerActive();
                    wireIcon.WireActiveFunc();
                    //continueBtn.SetActive(true);
                    switchIcon.SwitchOn();
                    //bulbComponent.lightOn();

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
        yield return new WaitForSeconds(3F);
        bulbComponent.lightOn();
        infoTextBox.text = "The light is on";

    }
    IEnumerator CapacitorTurnOffWait()

    {
        yield return new WaitForSeconds(3F);
        bulbComponent.lightOff();
        GetComponent<LineRenderer>().material = wireInactive;
        wireIcon.WireInactiveFunc();
        lightBulbTrigger.lightBulbIcon.LightBulbIconOff();


    }
    public void setCircuitInactive()
    {
        isCurrentRunning = false;
       
        batteryTrigger.batteryIcon.BatteryPowerInactive();
       
        switchIcon.SwitchOff();
        StartCoroutine(CapacitorTurnOffWait());




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
