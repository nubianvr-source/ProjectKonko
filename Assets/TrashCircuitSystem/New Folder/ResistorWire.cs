using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResistorWire : MonoBehaviour
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
    //public DiodeObjectScript diodeObject;
    public ResistorTrigger resistor;
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

    private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }

    public void setCircuitActive()
    {
        if (isBatteryInTrigger)
        {
            if (isLightBulbInTrigger)
            {

                if (resistor.isInTrigger)
                {
                    Color bulbcolor = bulbComponent.LightOnMat.color;
                    bulbcolor.r = coloNumberConversion(122);
                    bulbcolor.g = coloNumberConversion(122);
                    bulbcolor.b = coloNumberConversion(122);
                    bulbcolor.a = coloNumberConversion(255);
                    bulbComponent.LightOnMat.color = bulbcolor;
                    isCurrentRunning = true;
                    infoTextBox.text = "Nice, the resistor within the circuit has an ohm rating of 1000 ohms, can you see the difference?";
                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                    lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                    batteryTrigger.batteryIcon.BatteryPowerActive();
                    wireIcon.WireActiveFunc();
                    continueBtn.SetActive(true);
                    switchIcon.SwitchOn();
                    bulbComponent.lightOn();
                }


                else
                {
                    isCurrentRunning = true;
                    infoTextBox.text = "Try placing the resistor into the circuit";
                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                    lightBulbTrigger.lightBulbIcon.LightBulbIconOn();
                    batteryTrigger.batteryIcon.BatteryPowerActive();
                    wireIcon.WireActiveFunc();
                    //continueBtn.SetActive(true);
                    switchIcon.SwitchOn();
                    bulbComponent.lightOn();

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

        batteryTrigger.batteryIcon.BatteryPowerInactive();
        bulbComponent.lightOff();
        wireModel.GetComponent<MeshRenderer>().material = wireInactive;
        wireIcon.WireInactiveFunc();
        lightBulbTrigger.lightBulbIcon.LightBulbIconOff();

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
