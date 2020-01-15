using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIconScript : MonoBehaviour
{

    public Sprite switchIsOn;

    public Sprite switchIsOff;

    // Start is called before the first frame update
    void Start()
    {
        SwitchOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SwitchOn()
    {
        gameObject.GetComponent<Image>().sprite = switchIsOn;

    }

    public void SwitchOff()
    {
        gameObject.GetComponent<Image>().sprite = switchIsOff;

    }
}
