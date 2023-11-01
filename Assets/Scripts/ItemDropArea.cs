using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DragDropManager itm = eventData.pointerDrag.GetComponent<DragDropManager>();
        Destroy(itm.gameObject);
    }
}
