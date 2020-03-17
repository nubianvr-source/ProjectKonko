using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lesson3WireScriptBulbinSeries : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;

    public WireIconScript wireIcon;

    [Header("Baterry Properties")]
    public BatteryTriggerScript batteryTrigger1;
    public BatteryTriggerScript batteryTrigger2;
    public BatteryTriggerScript batteryTrigger3;
    private bool isBatteryInTrigger;

    [Header("Light Bulb Properties")]
    public LightBulbTriggerScript lightBulbTrigger1;
    public LightBulbTriggerScript lightBulbTrigger2;
    public LightBulbTriggerScript lightBulbTrigger3;
    private bool isLightBulbInTrigger;

    [Header("Diode Properties")]
    public DiodeObjectScript diodeObject;
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

    public void setCircuitActive()
    {
        if (isBatteryInTrigger)
        {
            if (lightBulbTrigger1.isInTrigger)
            {

                isCurrentRunning = true;
                infoTextBox.text = "Great!";
                GetComponent<LineRenderer>().material = LiveWire;
                lightBulbTrigger1.lightBulbIcon.LightBulbIconOn();
                batteryTrigger1.batteryIcon.BatteryPowerActive();
                wireIcon.WireActiveFunc();
                bulbComponent1.lightOn();
               
                switchIcon.SwitchOn();

                if (lightBulbTrigger2.isInTrigger)
                {
                    bulbComponent2.lightOn();


                    if (lightBulbTrigger3.isInTrigger)
                    {
                        bulbComponent3.lightOn(); 
                        continueBtn.SetActive(true);
                    }

                    else
                    {
                        bulbComponent1.lightOff();
                        bulbComponent2.lightOff();
                        bulbComponent3.lightOff();
                    }
                }

                else 
                {
                    bulbComponent1.lightOff();
                    bulbComponent2.lightOff();
                    bulbComponent3.lightOff();
                }

            }

            else
            {
                //Play a sound prompt or indicate error for no Light Bulb here
                infoTextBox.text = "Please place a LightBulb in the circuit"; 
                GetComponent<LineRenderer>().material = LiveWire;
                bulbComponent1.lightOff();
                bulbComponent2.lightOff();
                bulbComponent3.lightOff();
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
        GetComponent<LineRenderer>().material = wireInactive;
        lightBulbTrigger1.lightBulbIcon.LightBulbIconOff();
        batteryTrigger1.batteryIcon.BatteryPowerInactive();
        wireIcon.WireInactiveFunc();
        bulbComponent1.lightOff();
        bulbComponent2.lightOff();
        bulbComponent3.lightOff();
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
