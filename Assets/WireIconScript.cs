using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireIconScript : MonoBehaviour
{

    public Sprite WireActive;

    public Sprite WireInactive;
    // Start is called before the first frame update
    void Start()
    {
        WireInactiveFunc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void WireActiveFunc()
    {
        gameObject.GetComponent<Image>().sprite = WireActive;

    }

    public void WireInactiveFunc()
    {
        gameObject.GetComponent<Image>().sprite = WireInactive;

    }
}
