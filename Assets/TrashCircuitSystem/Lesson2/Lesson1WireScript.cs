using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lesson1WireScript : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;
    public GameObject wireModel;
    public WireIconScript wireIcon;

    [Header("Baterry Properties")]
    public BatteryTriggerScript batteryTrigger;
    private bool isBatteryInTrigger;

    [Header("Light Bulb Properties")]
    public LightBulbTriggerScript lightBulbTrigger;
    private bool isLightBulbInTrigger;

    [Header("Diode Properties")]
    public DiodeObjectScript diodeObject;
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
       // isDiodePositiveBias = diodeObject.isPositiveBias;
        
    }

    public void setCircuitActive()
    {
        if (isBatteryInTrigger)
        {
            if (isLightBulbInTrigger)
            {


                isCurrentRunning = true;
                infoTextBox.text = "Great Light Bulb came on";
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
