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

    public LightBulbComponent bulbComponent;

    public Text infoTextBox;

    private bool isBatteryInTrigger;

    private bool isLightBulbInTrigger;


    // Start is called before the first frame update
    void Start()
    {
        setCircuitInactive();
    }

    // Update is called once per frame
    void Update()
    {
        isBatteryInTrigger = batteryTrigger.isInTrigger;
        isLightBulbInTrigger = lightBulbTrigger.isInTrigger;
        
    }

    public void setCircuitActive()
    {
        if (isBatteryInTrigger)
        {
            if (isLightBulbInTrigger)
            {
                infoTextBox.text = "Circuit is On";
                GetComponent<LineRenderer>().material = LiveWire;
                lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                batteryTrigger.batteryIcon.BatteryPowerActive();
                wireIcon.WireActiveFunc();
                switchIcon.SwitchOn();
                //bulbComponent.lightBulbComp.GetComponent<MeshRenderer>().material = bulbComponent.LightOnMat;
                
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
        
        infoTextBox.text = "Circuit is Off";
        GetComponent<LineRenderer>().material = wireInactive;
        lightBulbTrigger.lightBulbIcon.LightBulbIconOff();
        batteryTrigger.batteryIcon.BatteryPowerInactive();
        wireIcon.WireInactiveFunc();
        switchIcon.SwitchOff();
        //bulbComponent.lightBulbComp.GetComponent<MeshRenderer>().material = bulbComponent.LightOnMat;



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
