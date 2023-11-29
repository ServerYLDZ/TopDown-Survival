using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ButtonScripts : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.DOFade(1, .3f);
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .3f);
        text.GetComponent<RectTransform>().DOAnchorPos3DY(80, .3f); 
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.DOFade(0, .3f);
        transform.DOScale(Vector3.one, .3f);
        text.GetComponent<RectTransform>().DOAnchorPos3DY(0, .3f);
      
    }
}
