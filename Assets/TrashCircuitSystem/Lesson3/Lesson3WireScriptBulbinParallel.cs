using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lesson3WireScriptBulbinParallel : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;

    public GameObject wireModel;

    public WireIconScript wireIcon;

    [Header("Baterry Properties")]
    public BatteryTriggerInitialized batteryTrigger1;
    private bool isBatteryInTrigger;

    [Header("Light Bulb Properties")]
    public LightBulbTriggerScript lightBulbTrigger1;
    public LightBulbTriggerScript lightBulbTrigger2;
    public LightBulbTriggerScript lightBulbTrigger3;
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

    public void setCircuitActive()
    {
        if (isBatteryInTrigger)
        {
            if (lightBulbTrigger1.isInTrigger)
            {

                isCurrentRunning = true;
                infoTextBox.text = "Great, now try adding another light bulb";
                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                lightBulbTrigger1.lightBulbIcon.LightBulbIconOn();
                batteryTrigger1.batteryIcon.BatteryPowerActive();
                wireIcon.WireActiveFunc();
                bulbComponent1.lightOn();
                //continueBtn.SetActive(true);
                switchIcon.SwitchOn();

                if (lightBulbTrigger2.isInTrigger)
                {
                    bulbComponent2.lightOn();
                    infoTextBox.text = "Awesome both light bulbs came on";
                    continueBtn.SetActive(true);

                    if (lightBulbTrigger3.isInTrigger)
                    {
                        bulbComponent3.lightOn();
                        continueBtn.SetActive(true);
                    }

                    else
                    {
                      
                        bulbComponent3.lightOff();
                    }
                }

                else
                {
 
                    bulbComponent2.lightOff();

                    if (lightBulbTrigger3.isInTrigger)
                    {
                        bulbComponent3.lightOn();
                        continueBtn.SetActive(true);
                    }

                    else
                    {

                        bulbComponent3.lightOff();
                    }

                }

            }

            else
            {
                //Play a sound prompt or indicate error for no Light Bulb here
                //infoTextBox.text = "Please place a LightBulb in the circuit";
                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                bulbComponent1.lightOff();
               
                if (lightBulbTrigger2.isInTrigger)
                {
                    bulbComponent2.lightOn();
                    continueBtn.SetActive(true);
                    infoTextBox.text = "First light bulb isnt in the circuit, but the second one is and has current flowing through it";
                    if (lightBulbTrigger3.isInTrigger)
                    {
                        bulbComponent3.lightOn();
                        continueBtn.SetActive(true);
                    }

                    else
                    {   
                         bulbComponent3.lightOff();
                    }
                }

                else
                {
                       bulbComponent2.lightOff();
                    infoTextBox.text = "There is no light bulb in the circuit";
                    if (lightBulbTrigger3.isInTrigger)
                    {
                        bulbComponent3.lightOn();
                        continueBtn.SetActive(true);
                    }

                    else
                    {
                      
                        bulbComponent3.lightOff();
                    }
                }

              
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
