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
    public Image imageFlip;


    public GameObject diodeObject;
    public Sprite positiveFlip;
    public Sprite negativeFlip;

    public Lesson4WireScriptWithDiodeSupport wireScript;

    // Start is called before the first frame update
    void Start()
    {
        diodeIcon.diodeActive = positiveBiasIcon;
        isPositiveBias = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flipSwitchUI()
    {
        wireScript.setCircuitInactive();
        print("Button has been clicked"); 
        isPositiveBias = !isPositiveBias;
        if (isPositiveBias)

        {
            imageFlip.GetComponent<Image>().sprite = positiveFlip;
            diodeIcon.diodeActive = positiveBiasIcon;
            diodeObject.transform.localEulerAngles = new Vector3(0, 0, -90);
           // 
            wireScript.setCircuitActive();

        }

        else 
        {
            imageFlip.GetComponent<Image>().sprite = negativeFlip;
            diodeIcon.diodeActive = negativeBiasIcon;
            diodeObject.transform.localEulerAngles = new Vector3(0, 0, 90);
           // wireScript.setCircuitInactive();
            wireScript.setCircuitActive();
        }
    }

    public void OnToggleChanged()
    {
       
        if (isPositiveBias)
        {
               
        



        }
        else
        {
               
        
           
        }
    
    }

    
}
