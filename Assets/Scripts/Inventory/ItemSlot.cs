using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public enum ItemSlotType
{
    EnvanterSlot,
    WeaponSlot
}
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex = 0;
    public DragDropManager dragableitem;
    public ItemSlotType slotType;
  
    public void OnDrop(PointerEventData eventData)
    {
        if (slotType == ItemSlotType.EnvanterSlot)
        {
            if (eventData.pointerDrag != null)
            {

                DragDropManager itm = eventData.pointerDrag.GetComponent<DragDropManager>();
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                if (transform.childCount == 0)
                {
                    itm.oldParent.GetComponent<ItemSlot>().dragableitem = null;
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;


                }
                else
                {

                    DragDropManager tmpıtem = dragableitem;
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;
                    if (transform.childCount > 0)
                        transform.GetChild(0).SetParent(itm.oldParent);




                    //yer değistirme

                }
              
            }
        }
        else
        {
            if (eventData.pointerDrag != null)
            {   
                DragDropManager itm = eventData.pointerDrag.GetComponent<DragDropManager>();
                if (itm.item.type == ItemTpe.Weapon) //eger weapon tipindeyse birakilan nesne
                {
                    if (transform.childCount == 0)//yani Bos El se
                    {

                        dragableitem = itm;
                        itm.parentAfterDrag = transform;

                        itm.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                        GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

                    }
                    else
                    {

                        DragDropManager tmpıtem = dragableitem;
                        dragableitem = itm;
                        itm.parentAfterDrag = transform;
                        if (transform.childCount > 0)
                            transform.GetChild(0).SetParent(itm.oldParent);

                 

                        itm.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                        GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                        //yer değistirme

                    }
                }

              

            }

            //wepon slot
        }

   
    }
}
