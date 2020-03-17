using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchComponent : MonoBehaviour
{
    public GameObject switchLever;
    public Material switchOnMat;
    public Material switchOffMat;
    public Button FlipButton;
    public Sprite buttonOff;
    public Sprite buttonOn;
    public bool switchBool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleBtnChange()
    {
        switchBool = !switchBool;
        if (switchBool)
        {
            switchLever.GetComponent<MeshRenderer>().material = switchOnMat;
            FlipButton.GetComponent<Image>().sprite = buttonOn;
        }
        else 
        {
            switchLever.GetComponent<MeshRenderer>().material = switchOffMat;
            FlipButton.GetComponent<Image>().sprite = buttonOff;
        }
        
    }
}
