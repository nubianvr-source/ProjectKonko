using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiodeIconScript : MonoBehaviour
{
    public Sprite diodeActive;

    public Sprite diodeInactive;

    // Start is called before the first frame update
    void Start()
    {
        diodeOutCircuit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void diodeInCircuit()
    {
        gameObject.GetComponent<Image>().sprite = diodeActive;

    }

    public void diodeOutCircuit()
    {
        gameObject.GetComponent<Image>().sprite = diodeInactive;

    }
}
