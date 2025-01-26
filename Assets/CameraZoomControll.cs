using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomControll : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            
        }
    }
}
