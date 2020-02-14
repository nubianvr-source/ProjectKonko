using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LightBulbIcon : MonoBehaviour
{

    [FormerlySerializedAs("LightBulbOn")] public Sprite lightBulbOn;

    [FormerlySerializedAs("LightBulbOff")] public Sprite lightBulbOff;

    // Start is called before the first frame update
    void Start()
    {
        LightBulbIconOff(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightBulbIconOn()
    {
        gameObject.GetComponent<Image>().sprite = lightBulbOn;
       
    }

    public void LightBulbIconOff()
    {
        gameObject.GetComponent<Image>().sprite = lightBulbOff;
    
    }
}
