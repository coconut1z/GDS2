﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 10f;
    private float panBorderTrigger = 10f;

    public Vector2 panLimit;

    // Update is called once per frame
    void Update () {

        Vector3 pos = transform.position;

        if(Input.mousePosition.y >= Screen.height - panBorderTrigger)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        else if (Input.mousePosition.y <= panBorderTrigger)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        else if (Input.mousePosition.x >= Screen.width - panBorderTrigger)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        else if (Input.mousePosition.x <= panBorderTrigger)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;   
    }
}