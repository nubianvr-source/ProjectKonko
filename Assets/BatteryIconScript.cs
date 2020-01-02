using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryIconScript : MonoBehaviour
{

    public Sprite batteryActive;

    public Sprite batteryInactive;


    // Start is called before the first frame update
    void Start()
    {
        BatteryPowerInactive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BatteryPowerActive()
    {
        gameObject.GetComponent<Image>().sprite = batteryActive;

    }

    public void BatteryPowerInactive()
    {
        gameObject.GetComponent<Image>().sprite = batteryInactive;

    }

}
