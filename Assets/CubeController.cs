using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Vector3 cubePosition;
    public float movementSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {   
        cubePosition = this.gameObject.transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cubePosition.x -= movementSpeed * Time.deltaTime; 
            print("Moving Left");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            cubePosition.x += movementSpeed * Time.deltaTime;
            print("Moving Right");
        }

        this.gameObject.transform.position = cubePosition;
    }
}
