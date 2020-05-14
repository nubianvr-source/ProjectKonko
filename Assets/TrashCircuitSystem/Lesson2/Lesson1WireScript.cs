using TMPro;
using UnityEngine;
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
    private bool isSeriesCircuitComplete;

    [Header("Resistor's Array")]
    public ResistorComponent[] resistors;
    private int resistorsInTriggers;
    private float totalCircuitResistance;


    [Header("Diode's Array")]
    public DiodeComponent diode;

    [Header("LED Componenet")]
    public LEDComponent LEDComp;


    [Header("Switch Properties")]
    public SwitchIconScript switchIcon;
    public TextMeshProUGUI infoTextBox;
    public SwitchComponent switchComponentRef;

    [Header("Other")]
    public LightBulbComponent bulbComponent;
    public GameObject continueBtn;
    public bool firstActivity = false;
    public bool secondActivityLesson2 = false;



    private bool batteryLoopThroughFunctionRun = false;



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

       


            for (int i = 0; i < batteries.Length; i++)
            {
                if (batteries[i].isInTrigger == true)
                {
                    batteryCircuitCount += 1;
                }
            }




            for (int i = 0; i < lightBulbs.Length; i++)
            {
                if (lightBulbs[i].isInSeries == true)
                {
                    if (lightBulbs[i].isInTrigger == true)
                    {
                        lightBulbsInTriggers += 1;
                        print("lightBulbinTriggers" + lightBulbsInTriggers);
                    }
                    else
                    {

                    }

                }
                else
                {
                    if (lightBulbs[i].isInTrigger == true)
                    {
                        lightBulbsInTriggers += 1;
                    }
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
            if (LEDComp != null)
            {

                if (LEDComp.isInTrigger)
                {
                    LEDComp.flipButton.gameObject.SetActive(true);
                    LEDComp.LEDDescription.gameObject.SetActive(false);

                    if (LEDComp.isPositiveBias)
                    {
                        infoTextBox.text = "The LED component is forward biased right now which is allowing current pass through the circuit. Try flipping the LED and observe what happens";

                    }
                    else
                    {
                        infoTextBox.text = "The LED component is reversed biased right now which and does not allow current to pass through the circuit. Try flipping the LED and observe what happens";
                    }


                }
                else
                {
                    infoTextBox.text = "Place LED into the circuit";
                }
            }

            else
            {
                var scene = SceneManager.GetActiveScene();
                int buildindex = scene.buildIndex;

                foreach (BatteryComponent batteryItem in batteries)
                {
                    if (batteryItem.isInTrigger)
                    {
                        if (buildindex == 3)
                        {
                            if (firstActivity)
                            {
                                //Casue the battery to explode 
                                batteryItem.batteryOverheating = true;
                                infoTextBox.text = "As you can see the battery is overheating, this is because there is no load in the circuit.";
                                continueBtn.SetActive(true);
                                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                            }

                            else
                            {
                                batteryItem.batteryOverheating = true;
                                infoTextBox.text = "There is no load in the circuit";
                                wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                            }


                        }
                        else
                        {

                            batteryItem.batteryOverheating = true;
                            infoTextBox.text = "There is no load in the circuit";
                            wireModel.GetComponent<MeshRenderer>().material = LiveWire;
                        }
                    }

                    else
                    {
                        infoTextBox.text = "Please a battety into the circuit.";
                    }


                }



            }


        }







        foreach (LightBulbComponent lightBulbItem in lightBulbs)
        {

            if (lightBulbItem.isInSeries)
            {
                if (lightBulbItem.isInTrigger == true)
                {
                    if (batteryCircuitCount >= minBatteryCount)
                    {
                        if (diode != null)
                        {
                            if (!diode.isInTrigger)
                            {
                                if (resistorsInTriggers >= 1)
                                {
                                    LightSwitchCheck(lightBulbItem);

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
                                    infoTextBox.text = "The diode is set to reverse bias, now flip and observe what happens";
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
                            if (resistorsInTriggers >= 1)
                            {
                                LightSwitchCheck(lightBulbItem);

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


                    }

                    else
                    {
                        var scene = SceneManager.GetActiveScene();
                        int buildindex = scene.buildIndex;
                        if (buildindex == 4)
                        {
                            // continueBtn.SetActive(true);
                            infoTextBox.text = "The battery voltage is not enough to power the lightbulb, we might need to add one more battery";
                        }
                        else
                        {
                            infoTextBox.text = "The battery voltage is not enough to power the lightbulb, we might need to add one more battery";
                        }


                    }


                }

                else
                {

                    var scene = SceneManager.GetActiveScene();
                    int buildindex = scene.buildIndex;
                    if (secondActivityLesson2)
                    {
                        infoTextBox.text = "The battery voltage is not enough to power the lightbulb, we might need to add one more battery";
                        continueBtn.SetActive(true);
                    }
                    else
                    {
                        infoTextBox.text = "The lightbulbs in a series connection is incomplete";
                        continueBtn.SetActive(false);
                        for (int i = 0; i < lightBulbs.Length; i++)
                        {
                            lightBulbs[i].lightOff();
                        }
                    }

                }
            }

            else
            {
                if (lightBulbItem.isInTrigger == true)
                {
                    if (batteryCircuitCount >= minBatteryCount)
                    {
                        if (diode != null)
                        {

                            if (!diode.isInTrigger)
                            {

                                if (resistorsInTriggers >= 1)
                                {

                                    LightSwitchCheck(lightBulbItem);

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
                                    infoTextBox.text = "The diode is set to reverse bias, now flip and observe what happens";
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
                            if (resistorsInTriggers >= 1)
                            {

                                LightSwitchCheck(lightBulbItem);

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




                    }

                    else
                    {

                        var scene = SceneManager.GetActiveScene();
                        int buildindex = scene.buildIndex;

                        switch (buildindex)
                        {
                            case 3:
                                if (secondActivityLesson2)
                                {
                                    infoTextBox.text = "The battery voltage is not enough to power the lightbulb, we might need to add one more battery";
                                    continueBtn.SetActive(true);
                                }

                                else
                                {
                                    infoTextBox.text = "The battery voltage is not enough to power the lightbulb, we might need to add one more battery";
                                }

                                break;
                            case 4:
                                infoTextBox.text = "The battery voltage is not enough to power the 3V lightbulb, since the batteries are arranged in parallel voltage isnt added up";
                                continueBtn.SetActive(true);
                                break;
                            default:
                                infoTextBox.text = "The lightbulbs in a series connection is incomplete";
                                for (int i = 0; i < lightBulbs.Length; i++)
                                {
                                    lightBulbs[i].lightOff();
                                }

                                break;
                        }

                    }

                }
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

    private void LightSwitchCheck(LightBulbComponent lightBulbItem)
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
        batteryLoopThroughFunctionRun = false;
        if (LEDComp != null)
        {
            LEDComp.flipButton.gameObject.SetActive(false);
            LEDComp.LEDDescription.gameObject.SetActive(true);
            LEDComp.TurnOffLED();

        }

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
