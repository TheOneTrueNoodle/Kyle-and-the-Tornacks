using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomControl : MonoBehaviour
{
    public float ZoomChange;
    public float SmoothChange;
    public float MinSize, MaxSize;

    private Camera cam;
    private CinemachineVirtualCamera virtualCam;

    private void Start()
    {
        virtualCam = gameObject.GetComponent<CinemachineVirtualCamera>();
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            virtualCam.m_Lens.OrthographicSize -= ZoomChange * Time.deltaTime * SmoothChange;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            virtualCam.m_Lens.OrthographicSize += ZoomChange * Time.deltaTime * SmoothChange;
        }

        virtualCam.m_Lens.OrthographicSize = Mathf.Clamp(virtualCam.m_Lens.OrthographicSize, MinSize, MaxSize);
    }
}
