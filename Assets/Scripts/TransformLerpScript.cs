using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLerpScript : MonoBehaviour
{

    public GameObject circuitComponent;
    private Vector3 startPos;
    float startPosX;
    public Vector3 endPos;
    public float lerpTime;
    private float distanceToMove;
    private float currentLerpTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        startPosX = circuitComponent.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
