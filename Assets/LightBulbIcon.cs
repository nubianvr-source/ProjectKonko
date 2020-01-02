using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBulbIcon : MonoBehaviour
{

    public Sprite LightBulbOn;

    public Sprite LightBulbOff;

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
        gameObject.GetComponent<Image>().sprite = LightBulbOn;
       
    }

    public void LightBulbIconOff()
    {
        gameObject.GetComponent<Image>().sprite = LightBulbOff;
    
    }
}
