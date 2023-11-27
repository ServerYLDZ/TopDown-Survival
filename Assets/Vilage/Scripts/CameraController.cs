using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera VirtualCam;
    CinemachineComponentBase componentBase;
    float camDis;
    public float sensivity = 10f;
        private void Update()
    {
        if (componentBase==null)
        {
            componentBase = VirtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
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
    /*  public Transform target;

      public float smoothSpeed = 8f;
      public Vector3 offset;

      void Update()
      {
          if(target == null) return;

          Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
          Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
          transform.position = smoothedPosition;
      }*/

}
