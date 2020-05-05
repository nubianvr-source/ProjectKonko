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

    public GameObject wireModel;

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
        infoTextBox.text = "Select the electric component you would like to use";
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

                infoTextBox.text = "Hmmm, it seems we need one more light bulb to complete the circuit";

                switchIcon.SwitchOn();

                if (lightBulbTrigger2.isInTrigger)
                {
                    infoTextBox.text = "The circuit is still not complete, we will need to add one more light bulb";


                    if (lightBulbTrigger3.isInTrigger)
                    { 
                        isCurrentRunning = true;
                        infoTextBox.text = "Great!, click the finish button to continue";
                        wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                        lightBulbTrigger1.lightBulbIcon.LightBulbIconOn();
                        batteryTrigger1.batteryIcon.BatteryPowerActive();
                        wireIcon.WireActiveFunc();
                        bulbComponent1.lightOn();

                        bulbComponent2.lightOn();
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
                infoTextBox.text = "There appear to be no light bulbs in the circuit";
                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                bulbComponent1.lightOff();
                bulbComponent2.lightOff();
                bulbComponent3.lightOff();
            }

        }
        else
        {
            //Play a sound prompt or indicate error for no battery in circuit here
            infoTextBox.text = "There is no power source in circuit right now, please place the battery on your left side into the circuit";
           

        }


    }

    public void setCircuitInactive()
    {
        isCurrentRunning = false;
        infoTextBox.text = "Circuit is Off right now, Turn it on by clicking on the big red Off button on your right to turn it on";
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
