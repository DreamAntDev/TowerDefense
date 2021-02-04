using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    [SerializeField] private float zoomLerpSpeed = 10;
    float scrollData;
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    
    }

    private void OnMouseUp() {
        Debug.Log("DRAGUP");
    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        cam.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    
}
