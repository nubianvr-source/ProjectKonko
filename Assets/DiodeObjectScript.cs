using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiodeObjectScript : MonoBehaviour
{
    [HideInInspector]
    public bool isPositiveBias;

    public DiodeIconScript diodeIcon;

    public Sprite positiveBiasIcon;

    public Sprite negativeBiasIcon;

    public Text diodeText;

    public Toggle toggle;

    public Material positiveBiasMat;

    public Material negativeBiasMat;

    public GameObject diodeObject;

    public WireScript wireScript;

    // Start is called before the first frame update
    void Start()
    {
        diodeIcon.diodeActive = positiveBiasIcon;
        isPositiveBias = true;
        toggle.GetComponent<Toggle>().isOn = isPositiveBias;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToggleChanged(bool toggle)
    {
        if (toggle)
        {

            if (wireScript.isCurrentRunning)
            {
                isPositiveBias = toggle;
                diodeIcon.diodeActive = positiveBiasIcon;
                diodeText.text = "Positive Bias";
                diodeObject.GetComponent<MeshRenderer>().material = positiveBiasMat;
                wireScript.setCircuitActive();
                wireScript.infoTextBox.text = "Circuit on";
            }

            else
            { 
             isPositiveBias = toggle;
            diodeIcon.diodeActive = positiveBiasIcon;
            diodeText.text = "Positive Bias";
            diodeObject.GetComponent<MeshRenderer>().material = positiveBiasMat;
            }
           


        }
        else
        {
            if (wireScript.isCurrentRunning)
            {
                isPositiveBias = toggle;
                diodeIcon.diodeActive = negativeBiasIcon;
                diodeText.text = "Negative Bias";
                diodeObject.GetComponent<MeshRenderer>().material = negativeBiasMat;
                wireScript.setCircuitInactive();
                wireScript.infoTextBox.text = "Circuit on but diode set in negative or reverse bias";
            }

            else
            {
                isPositiveBias = toggle;
                diodeIcon.diodeActive = negativeBiasIcon;
                diodeText.text = "Negative Bias";
                diodeObject.GetComponent<MeshRenderer>().material = negativeBiasMat;
            }
           
        }
    
    }

    
}
