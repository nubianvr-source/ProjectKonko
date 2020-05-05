using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lesson3WireScriptBatteryinSeries : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;

    public GameObject wireModel;

    public WireIconScript wireIcon;

    [Header("Baterry Properties")]
    public BatteryTriggerScript batteryTrigger1;
    public BatteryTriggerScript batteryTrigger2;
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
            Color bulbcolor = bulbComponent1.LightOnMat.color;
            bulbcolor.r = coloNumberConversion(77);
            bulbcolor.g = coloNumberConversion(77);
            bulbcolor.b = coloNumberConversion(77);
            bulbcolor.a = coloNumberConversion(255);
            bulbComponent1.LightOnMat.color = bulbcolor;
            infoTextBox.text = "Nice, Let's Try adding one more battery cell and observe what happens. Turn off the circuit to continue";
            LightBulbOn();
         
            if (batteryTrigger2.isInTrigger)
            {

                bulbcolor.r = coloNumberConversion(125);
                bulbcolor.g = coloNumberConversion(125);
                bulbcolor.b = coloNumberConversion(125);
                bulbcolor.a = coloNumberConversion(255);
                bulbComponent1.LightOnMat.color = bulbcolor;
                infoTextBox.text = "Add one more battery cell to see the light bulb at full brightness";
                LightBulbOn();
                if (batteryTrigger3.isInTrigger)
                {
                    bulbcolor.r = coloNumberConversion(255);
                    bulbcolor.g = coloNumberConversion(255);
                    bulbcolor.b = coloNumberConversion(255);
                    bulbcolor.a = coloNumberConversion(255);
                    bulbComponent1.LightOnMat.color = bulbcolor;
                    LightBulbOn();
                    continueBtn.SetActive(true);
                    infoTextBox.text = "Great, Click Finish to continue";
                }
            }
        }

        else 
        {
            //Play a sound prompt or indicate error for no Light Bulb here
            infoTextBox.text = "There is no power source in circuit right now, please place the battery on your left side into the circuit";

        }


    }

    public void LightBulbOn()
    {
        if (lightBulbTrigger1.isInTrigger)
        {

            isCurrentRunning = true;
           
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
