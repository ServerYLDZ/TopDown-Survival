using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{

    CinemachineComponentBase componentBase;
    float camDis;
    public float sensivity = 10f;
        private void Update()
    {
        if (componentBase==null)
        {
            componentBase = GameManager.Instance.VirtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
        if (Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            camDis = Input.GetAxis("Mouse ScrollWheel") * sensivity;
            if (componentBase is CinemachineFramingTransposer)
            {
                if ((componentBase as CinemachineFramingTransposer).m_CameraDistance-camDis>5 && (componentBase as CinemachineFramingTransposer).m_CameraDistance - camDis < 20)
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= camDis;
            }
        }
    }
  

}
