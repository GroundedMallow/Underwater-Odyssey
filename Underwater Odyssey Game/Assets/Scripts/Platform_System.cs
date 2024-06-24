using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_System : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = .2f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = .2f;
            }
            else
            {
                waitTime -= Time.deltaTime; //decrease value
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            effector.rotationalOffset = 0;
        }
    }
}
