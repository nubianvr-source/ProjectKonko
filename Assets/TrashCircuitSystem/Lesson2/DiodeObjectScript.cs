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

    public GameObject diodeObject;
    public Sprite positiveFlip;
    public Sprite negativeFlip;

    public Lesson4WireScriptWithDiodeSupport wireScript;

    // Start is called before the first frame update
    void Start()
    {
        diodeIcon.diodeActive = positiveBiasIcon;
        isPositiveBias = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flipSwitchUI()
    {
        if (isPositiveBias)

        {
            this.GetComponent<Image>().sprite = positiveFlip;



        }

        else 
        {
            this.GetComponent<Image>().sprite = negativeFlip;
        }
    }

    public void OnToggleChanged()
    {
        isPositiveBias = !isPositiveBias;
        if (isPositiveBias)
        {
               
         diodeIcon.diodeActive = positiveBiasIcon;
         diodeObject.transform.Rotate(0, 0, 0);



        }
        else
        {
               
         diodeIcon.diodeActive = negativeBiasIcon;
         diodeObject.transform.Rotate(0,180,0);
           
        }
    
    }

    
}
