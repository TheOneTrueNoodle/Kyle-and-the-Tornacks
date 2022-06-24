using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMoveControl : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;

    private CinemachineVirtualCamera virtualCam;
    private Camera cam;

    private bool drag = false;

    private void Start()
    {
        cam = Camera.main;
        virtualCam = gameObject.GetComponent<CinemachineVirtualCamera>();
        ResetCamera = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        if(Input.GetMouseButton(1))
        {
            Difference = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            if (drag == false)
            {
                drag = true;
                Origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if(drag)
        {
            virtualCam.transform.position = Origin - Difference;
        }
    }
}
