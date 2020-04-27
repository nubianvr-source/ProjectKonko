using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbBlock : CircuitSystemBlock
{
    // set these values in the inspector
    public Material LightOn; // the object that represents the lightbulb turned on
    public Material LightOff; // the object that represents the lightbulb turned off
    public GameObject self;
    public override void Init()
    {
        //Get a refernce to sself GameObject
        //self = GetComponent<GameObject>();
        //The light will initially be turned off
        //Value = 0;
        
    }

    public override bool IsSwitchedOn()
    {
        // the light will only turn on if all inputs are greater than 0
        foreach (CircuitSystemBlock input in CompBlock)
            if (input.Value <= 0)
            {
                 return false;
            }
           
      return true;
    }


 public override void OffAction()
    {
        if (self.GetComponent<MeshRenderer>().material = LightOn)
        {
            self.GetComponent<MeshRenderer>().material = LightOff;
            Value = 0;
           
        }
    }

    public override void OnAction()
    {
        if (self.GetComponent<MeshRenderer>().material = LightOff)
        {
            self.GetComponent<MeshRenderer>().material = LightOn;
            Value = 1;
            
        }
    }

   
}
