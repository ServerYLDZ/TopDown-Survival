using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ActorControlItem : MonoBehaviour ,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    public Ally ally;
    public Image image;
    public TMP_Text Name;
    public TMP_Text Healt;
    public TMP_Text Hunger;
    public TMP_Text currentState;
    public Transform parentAfterDrag;
    public CanvasGroup cncsGroup;

    private void Start()
    {
        GameManager.Instance.ActorControler.RefreshActorControlPrefab(ally, this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ally.myClass != ActorClass.Leader)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            cncsGroup.blocksRaycasts = false;
            cncsGroup.alpha = .6f;
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ally.myClass != ActorClass.Leader )
            transform.position = Input.mousePosition;
    }

   

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ally.myClass != ActorClass.Leader)
        {
            transform.SetParent(parentAfterDrag);
            cncsGroup.blocksRaycasts = true;
            cncsGroup.alpha = 1;
        }
          
    }
}
