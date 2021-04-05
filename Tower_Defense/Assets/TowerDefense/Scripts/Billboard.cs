﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] protected Transform cam;

    private void Start() {
        cam = Camera.main.transform;
    }

    
    private void LateUpdate() {
        transform.LookAt(transform.position + cam.forward);
    }
}
