using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbComponent : MonoBehaviour
{

    public GameObject lightBulbComp;
    public Material LightOffMat;
    public Material LightOnMat;

    // Start is called before the first frame update
    void Start()
    {
        lightOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lightOn() 
    {
        lightBulbComp.GetComponent<MeshRenderer>().material = LightOnMat;
    }

    public void lightOff()
    {
        lightBulbComp.GetComponent<MeshRenderer>().material = LightOffMat;
    }
}
