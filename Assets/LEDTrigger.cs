using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDTrigger : MonoBehaviour
{

    //Light bulb transform reference
    public Transform LEDTransform;

    public bool isInTrigger;

    

    private void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LED")
        {
            FindObjectOfType<SoundManager>().Play("SuccessTrigger");
            var LED = other.GetComponent<LEDComponent>();
            LED.LEDTriggerReference = this;
            LED.LEDTransform = LEDTransform;
            LED.isInTrigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LED")
        {
            FindObjectOfType<SoundManager>().Play("");
            var LED = other.GetComponent<LEDComponent>();
            LED.LEDTriggerReference = this;
            LED.isInTrigger = false;

        }
    }


}
