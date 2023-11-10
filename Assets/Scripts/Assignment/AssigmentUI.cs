using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class AssigmentUI : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Ally ally;
    public Image image;
    public TMP_Text Name;
    public TMP_Text jobText;
    public Transform parentAfterDrag;
    public Transform oldParent;
    public CanvasGroup cncsGroup;
   

    public void OnBeginDrag(PointerEventData eventData)
    {
            oldParent = transform.parent;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            cncsGroup.blocksRaycasts = false;
            cncsGroup.alpha = .6f;
        
      
    }

    public void OnDrag(PointerEventData eventData)
    {
 
            transform.position = Input.mousePosition;
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        
            transform.SetParent(parentAfterDrag);
            cncsGroup.blocksRaycasts = true;
            cncsGroup.alpha = 1;
        
         
    }
}
