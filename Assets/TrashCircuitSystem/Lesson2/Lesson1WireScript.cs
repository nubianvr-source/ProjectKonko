using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lesson1WireScript : MonoBehaviour
{
    [Header("Wire Properties")]
    public Material wireInactive;

    public Material LiveWire;
    public GameObject wireModel;
    public WireIconScript wireIcon;

    [Header("Baterry's Array")]
    public BatteryComponent[] batteries;
    private int batteryCircuitCount;
    public int minBatteryCount = 1;

    [Header("Light Bulb's Array")]
    public LightBulbComponent[] lightBulbs;
    private int lightBulbsInTriggers;

    [Header("Resistor's Array")]
    public ResistorComponent[] resistors;
    private int resistorsInTriggers;
    private float totalCircuitResistance;


    [Header("Diode's Array")]
    public DiodeComponent diode;

   
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

        toggleChanged();
    }
    private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }
    public void setCircuitActive()
    {
     

        foreach (BatteryComponent batteryItem in batteries)
        {
            if (batteryItem.isInTrigger == true)
            {
                batteryCircuitCount += 1;
                for (int i = 0; i < lightBulbs.Length; i++)
                {
                    if (lightBulbs[i].isInTrigger == true)
                    {
                        lightBulbsInTriggers += 1;
                    }
                }

                for (int i = 0; i < resistors.Length; i++)
                {
                    if (resistors[i].isInTrigger == true)
                    {
                        totalCircuitResistance = totalCircuitResistance + resistors[i].resistanceFloat;
                        resistorsInTriggers += 1;
                    }
                }


                if (lightBulbsInTriggers <= 0 && resistorsInTriggers <= 0)
                {
                    var scene = SceneManager.GetActiveScene();
                    int buildindex = scene.buildIndex;
                    if (buildindex == 3)
                    {
                        //Casue the battery to explode 
                        batteryItem.batteryOverheating = true;
                        infoTextBox.text = "As you can see the battery is overheating, this is because there is no load in the circuit.";
                        continueBtn.SetActive(true);

                    }
                    else {

                        batteryItem.batteryOverheating = true;
                        infoTextBox.text = "There is no load in the circuit";
                    }
                   
                }
            }
        }

        foreach (LightBulbComponent lightBulbItem in lightBulbs)
        {
            if (lightBulbItem.isInTrigger == true)
            {
                if (batteryCircuitCount >= minBatteryCount)
                {
                    if (!diode.isInTrigger)
                    {

                        if (resistorsInTriggers >= 1)
                        {
                            
                            switch (totalCircuitResistance)
                            {
                              
                                case 750:
                                    Color bulbcolor = lightBulbItem.LightOnMat.color;
                                    bulbcolor.r = coloNumberConversion(200);
                                    bulbcolor.g = coloNumberConversion(200);
                                    bulbcolor.b = coloNumberConversion(200);
                                    bulbcolor.a = coloNumberConversion(255);
                                    lightBulbItem.LightOnMat.color = bulbcolor;
                                    lightBulbItem.lightOn();
                                    isCurrentRunning = true;
                                    infoTextBox.text = "Great, now try swapping the resistor and observe what happens";
                                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                                    wireIcon.WireActiveFunc();
                                    break;
                                case 1500:
                                    bulbcolor = lightBulbItem.LightOnMat.color;
                                    bulbcolor.r = coloNumberConversion(150);
                                    bulbcolor.g = coloNumberConversion(150);
                                    bulbcolor.b = coloNumberConversion(150);
                                    bulbcolor.a = coloNumberConversion(150);
                                    lightBulbItem.LightOnMat.color = bulbcolor;
                                    lightBulbItem.lightOn();
                                    isCurrentRunning = true;
                                    infoTextBox.text = "Great, now try swapping the resistor and observe what happens";
                                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                                    wireIcon.WireActiveFunc();
                                    continueBtn.SetActive(true);
                                    break;

                                case 3000:
                                    bulbcolor = lightBulbItem.LightOnMat.color;
                                    bulbcolor.r = coloNumberConversion(100);
                                    bulbcolor.g = coloNumberConversion(100);
                                    bulbcolor.b = coloNumberConversion(100);
                                    bulbcolor.a = coloNumberConversion(100);
                                    lightBulbItem.LightOnMat.color = bulbcolor;
                                    lightBulbItem.lightOn();
                                    isCurrentRunning = true;
                                    infoTextBox.text = "Great, now try swapping the resistor and observe what happens";
                                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                                    wireIcon.WireActiveFunc();
                                    continueBtn.SetActive(true);
                                    break;

                                default:
                                lightBulbItem.lightOff();
                                isCurrentRunning = true;
                                infoTextBox.text = "Great, there is current in the circuit but the resistor is preventing the light bulb from lightening up due to its resistance in the circuit";
                                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                                wireIcon.WireActiveFunc();
                                    break;
                            }
                           
                        }

                        else
                        {
                            lightBulbItem.lightOn();
                            isCurrentRunning = true;
                            infoTextBox.text = "Great Light Bulb came on";
                            wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                            wireIcon.WireActiveFunc();
                            //bulbComponent.lightOn();
                            continueBtn.SetActive(true);
                            // switchIcon.SwitchOn();
                        }
                    }
                    else
                    {
                        if (diode.isPositiveBias)
                        {
                            if (resistorsInTriggers >= 1)
                            {
                                if (totalCircuitResistance >= 3000)
                                {
                                    lightBulbItem.lightOff();
                                    isCurrentRunning = true;
                                    infoTextBox.text = "Great, there is current in the circuit but the resistor is preventing the light bulb from lightening up due to its resistance in the circuit";
                                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                                    wireIcon.WireActiveFunc();
                                    //bulbComponent.lightOn();
                                    //continueBtn.SetActive(true);
                                    // switchIcon.SwitchOn();
                                }
                                else
                                {
                                    lightBulbItem.lightOn();
                                    isCurrentRunning = true;
                                    infoTextBox.text = "Great Light Bulb came on";
                                    wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                                    wireIcon.WireActiveFunc();
                                }
                            }

                            else
                            {
                                lightBulbItem.lightOn();
                                isCurrentRunning = true;
                                infoTextBox.text = "Great Light Bulb came on, the diode is set to reverse bias, flip the diode a few more times and observe what happens before you continue";
                                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                                wireIcon.WireActiveFunc();
                                //bulbComponent.lightOn();
                                continueBtn.SetActive(true);
                                // switchIcon.SwitchOn();
                            }
                        }
                        else
                        {
                            lightBulbItem.lightOff();
                            isCurrentRunning = true;
                            infoTextBox.text = "The diode is set to reverse bias, now flip and observe waht happens";
                            wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                            wireIcon.WireActiveFunc();
                            //bulbComponent.lightOn();
                            //continueBtn.SetActive(true);
                            // switchIcon.SwitchOn();

                        }
                    }
                   
                    
                }
                else
                {

                    var scene = SceneManager.GetActiveScene();
                    int buildindex = scene.buildIndex;
                    if (buildindex == 3)
                    {
                        infoTextBox.text = "The battery voltage is not enough to power the lightbulb, we might need to add one more battery";
                        continueBtn.SetActive(true);
                    }
                    else
                    { 
                        infoTextBox.text = "Please place the battery in the circuit to continue";
                    }
                        
                }
                
            }
            else 
            {
                infoTextBox.text = "Please place the LightBulb in the circuit";
            }
        }


        /*if (isBatteryInTrigger)
        {
            if (isLightBulbInTrigger)
            {


                isCurrentRunning = true;
                infoTextBox.text = "Great Light Bulb came on";
                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                wireIcon.WireActiveFunc();
                //bulbComponent.lightOn();
                continueBtn.SetActive(true);
               // switchIcon.SwitchOn();
                
                
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
       */
    
    }

    public void setCircuitInactive()
    {
        isCurrentRunning = false;
        infoTextBox.text = "Circuit is Off";
        wireModel.GetComponent<MeshRenderer>().material = wireInactive;
        wireIcon.WireInactiveFunc();
        batteryCircuitCount = 0;
        resistorsInTriggers = 0;
        lightBulbsInTriggers = 0;
        //bulbComponent.lightOff();
        //switchIcon.SwitchOff();

        foreach (LightBulbComponent lightBulbItem in lightBulbs)
        {
            if (lightBulbItem.isInTrigger == true)
            {
                lightBulbItem.lightOff();
            }
            else
            {

            }
        }

        foreach (BatteryComponent batteryItem in batteries)
        {
            if (batteryItem.isInTrigger == true)
            {

                //Casue the battery to explode 
                batteryItem.batteryOverheating = false;

            }
        }
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
