using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lesson4WireScriptWithDiodeSupport : MonoBehaviour
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
    public DiodeComponent diodeObject;
    public DiodeTriggerScript diodeTrigger;
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
        isDiodePositiveBias = diodeObject.isPositiveBias;

    }

    public void setCircuitActive()
    {
        if (isBatteryInTrigger)
        {
            if (isLightBulbInTrigger)
            {

                if (isDiodePositiveBias)
                {
                    isCurrentRunning = true;
                    infoTextBox.text = "Great, The Light Bulb came on. Now turn the circuit off and flip the diode to see what what happens";
                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                    lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                    batteryTrigger.batteryIcon.BatteryPowerActive();
                    wireIcon.WireActiveFunc();
                    bulbComponent.lightOn();
                    continueBtn.SetActive(true);
                    switchIcon.SwitchOn();

                }


                else
                {
                    infoTextBox.text = "The diode has been set in the reverse bias, flip the diode to see what happens";

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

    public void setCircuitInactive()
    {
        isCurrentRunning = false;
        infoTextBox.text = "Circuit is Off";
        wireModel.GetComponent<MeshRenderer>().material = wireInactive;
        lightBulbTrigger.lightBulbIcon.LightBulbIconOff();
        batteryTrigger.batteryIcon.BatteryPowerInactive();
        wireIcon.WireInactiveFunc();
        bulbComponent.lightOff();
        switchIcon.SwitchOff();




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
