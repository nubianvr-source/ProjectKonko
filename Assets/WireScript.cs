using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireScript : MonoBehaviour
{
    public Material wireInactive;

    public Material LiveWire;

    public WireIconScript wireIcon;

    public BatteryTriggerScript batteryTrigger;

    public LightBulbTriggerScript lightBulbTrigger;

    public SwitchIconScript switchIcon;

    public DiodeObjectScript diodeObject;

    public DiodeTriggerScript diodeTrigger;

    public LightBulbComponent bulbComponent;

    public GameObject continueBtn;

    public Text infoTextBox;

    private bool isBatteryInTrigger;

    private bool isLightBulbInTrigger;

    private bool isDiodePositiveBias;

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


                isCurrentRunning = true;
                infoTextBox.text = "Great Light Bulb came on";
                GetComponent<LineRenderer>().material = LiveWire;
                lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                batteryTrigger.batteryIcon.BatteryPowerActive();
                wireIcon.WireActiveFunc();
                bulbComponent.lightOn();
                continueBtn.SetActive(true);
                switchIcon.SwitchOn();
                
                
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
        GetComponent<LineRenderer>().material = wireInactive;
        lightBulbTrigger.lightBulbIcon.LightBulbIconOff();
        batteryTrigger.batteryIcon.BatteryPowerInactive();
        wireIcon.WireInactiveFunc();
        bulbComponent.lightOff();
        switchIcon.SwitchOff();
        



    }

    public void toggleChanged(bool BoolValue) 
    {
        if (BoolValue)
        {
           
            setCircuitActive();

        }
        else
        {
            
            setCircuitInactive();
        }
    
    }
}
