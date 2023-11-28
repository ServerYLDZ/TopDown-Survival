using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FloatingText : MonoBehaviour
{
    public float time=2;
    public float speed=2;

    
    CinemachineComponentBase componentBase;
    private void OnEnable()
    {
        //StartCoroutine(InactiveText());
        Invoke(nameof(InactiveText), time);
        componentBase =GameManager.Instance.VirtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if ((componentBase as CinemachineFramingTransposer).m_CameraDistance > 10)
            GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
        else
            GetComponent<RectTransform>().localScale = Vector3.one;
    }
    private void Update() {
        transform.position+=new Vector3(0,speed*Time.deltaTime,0);
    }

    public void InactiveText()
    {
        if (this.isActiveAndEnabled)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(ObjectPool.Instance.gameObject.transform);
        }
    }
  /*  IEnumerator InactiveText(){
        yield return new WaitForSeconds(time);
        if (this.isActiveAndEnabled)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(ObjectPool.Instance.gameObject.transform);
        }
        
    }*/


}
