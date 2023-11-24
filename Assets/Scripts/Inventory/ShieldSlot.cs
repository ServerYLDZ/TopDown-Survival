using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShieldSlot : MonoBehaviour, IDropHandler
{
    public DragDropManager dragableitem;

    public void OnDrop(PointerEventData eventData)
    {
        DragDropManager itm = eventData.pointerDrag.GetComponent<DragDropManager>();
        if (eventData.pointerDrag != null)
        {
            if (itm.item.type == ItemTpe.Armor && itm.item.prefab.GetComponent<Armor>().armorType==Armor.ArmorType.Shield ) // ormorsa ve kalkansa
            {
                if (dragableitem==null)//yani Bos El se
                {

                    dragableitem = itm;
                    itm.parentAfterDrag = transform;

                    itm.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                    GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

                }
                else
                {

                    dragableitem.item.prefab.GetComponent<Armor>().UnUseArmor(GameManager.Instance.CurrentActor);
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;
                    if (transform.childCount > 0)
                        transform.GetChild(0).SetParent(itm.oldParent);



                    itm.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                    GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                   
                    //yer degistirme
                }
            }
        }
    }
}
