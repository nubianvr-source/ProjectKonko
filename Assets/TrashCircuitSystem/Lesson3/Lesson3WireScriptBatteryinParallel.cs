using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lesson3WireScriptBatteryinParallel : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;

    public GameObject wireModel;

    public WireIconScript wireIcon;

    [Header("Baterry Properties")]
    public BatteryTriggerLesson3 batteryTrigger1;
    public BatteryTriggerLesson3 batteryTrigger2;
    public BatteryTriggerScript batteryTrigger3;
    private bool isBatteryInTrigger;

    [Header("Light Bulb Properties")]
    public LLightBulbTriggerBulbInitiallized lightBulbTrigger1;
    private bool isLightBulbInTrigger;

    [Header("Diode Properties")]
    public DiodeComponent diodeObject;
    public DiodeTriggerScript diodeTrigger;


    [Header("Switch Properties")]
    public SwitchIconScript switchIcon;
    public TextMeshProUGUI infoTextBox;
    public SwitchComponent switchComponentRef;

    [Header("Other")]
    public LightBulbComponent bulbComponent1;
    public LightBulbComponent bulbComponent2;
    public LightBulbComponent bulbComponent3;
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
        isBatteryInTrigger = batteryTrigger1.isInTrigger;
        isLightBulbInTrigger = lightBulbTrigger1.isInTrigger;
        // isDiodePositiveBias = diodeObject.isPositiveBias;

    }
    private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }


    public void setCircuitActive()
    {
        if (batteryTrigger1.isInTrigger)
        {
          
            LightBulbOn();
            continueBtn.SetActive(true);
            infoTextBox.text = "Great, You can place the the other battery in the circuit or click continue to move on";
            if (batteryTrigger2.isInTrigger)
            {

                LightBulbOn();
                continueBtn.SetActive(true);
                infoTextBox.text = "Great, Lets Continue";
            }
        }

        else
        {
            if (batteryTrigger2.isInTrigger)
            {

                LightBulbOn();
                continueBtn.SetActive(true);
                infoTextBox.text = "Great, You can place the the other battery in the circuit or click continue to move on";
            }
        }


    }

    public void LightBulbOn()
    {
        if (lightBulbTrigger1.isInTrigger)
        {

            isCurrentRunning = true;
            infoTextBox.text = "Great!";
            wireModel.GetComponent<MeshRenderer>().material = LiveWire;
            lightBulbTrigger1.lightBulbIcon.LightBulbIconOn();
            batteryTrigger1.batteryIcon.BatteryPowerActive();
            wireIcon.WireActiveFunc();
            bulbComponent1.lightOn();

            switchIcon.SwitchOn();

        }

        else
        {
            //Play a sound prompt or indicate error for no Light Bulb here
            infoTextBox.text = "Please place a LightBulb in the circuit";
            wireModel.GetComponent<MeshRenderer>().material = LiveWire;

        }

    }

    public void setCircuitInactive()
    {
        isCurrentRunning = false;
        infoTextBox.text = "Circuit is Off";
        wireModel.GetComponent<MeshRenderer>().material = wireInactive;
        lightBulbTrigger1.lightBulbIcon.LightBulbIconOff();
        batteryTrigger1.batteryIcon.BatteryPowerInactive();
        wireIcon.WireInactiveFunc();
        bulbComponent1.lightOff();
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
