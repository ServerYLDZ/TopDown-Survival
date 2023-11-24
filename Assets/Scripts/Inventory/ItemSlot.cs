using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
  
    public DragDropManager dragableitem;

  
    public void OnDrop(PointerEventData eventData)
    {
        DragDropManager itm = eventData.pointerDrag.GetComponent<DragDropManager>();
        if (!itm.oldParent.GetComponent<WeaponSlot>() && !itm.oldParent.GetComponent<ShieldSlot>()&& !itm.oldParent.GetComponent<HeadSlot>()&& !itm.oldParent.GetComponent<ChestSlot>()&& !itm.oldParent.GetComponent<FootSlot>()) //itemimin eski parenti  weponslot değilse
        {
            if (eventData.pointerDrag != null)
            {

             
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                if (transform.childCount == 0) //el bossa
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
                    itm.oldParent.GetComponent<ItemSlot>().dragableitem = tmpıtem;
                 


                    //yer değistirme

                }
              
            }
        }
        else if (itm.oldParent.GetComponent<WeaponSlot>()) //weapon slottan geldimi
        {
            if (eventData.pointerDrag != null)
            {


                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                if (dragableitem==null) //el bossa
                {
                    itm.oldParent.GetComponent<WeaponSlot>().dragableitem = null;
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;


                }
                else
                {
                   
                    if (dragableitem.item.type == ItemTpe.Weapon ) //ustundeki weoponsa yerdegis deilse gei koyama;
                    {
                        dragableitem.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                        GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

                        DragDropManager tmpıtem = dragableitem;
                        dragableitem = itm;
                        itm.parentAfterDrag = transform;
                        if (transform.childCount > 0)
                            transform.GetChild(0).SetParent(itm.oldParent);
                        itm.oldParent.GetComponent<WeaponSlot>().dragableitem = tmpıtem;




                        //yer degistirme

                    }

                }

            }
        }
        else if (itm.oldParent.GetComponent<ShieldSlot>()) //shiled slottan geldimi
        {
            if (eventData.pointerDrag != null)
            {
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                if (transform.childCount == 0) //el bossa
                {
                    itm.oldParent.GetComponent<ShieldSlot>().dragableitem.item.prefab.GetComponent<Armor>().UnUseArmor(GameManager.Instance.CurrentActor); //zirhi cikar
                    itm.oldParent.GetComponent<ShieldSlot>().dragableitem = null;
               
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;
              
                    

                }
                else
                {
                    if (dragableitem.item.type == ItemTpe.Armor && dragableitem.item.prefab.GetComponent<Armor>().armorType == Armor.ArmorType.Shield) //ustundeki shield yerdegis deilse gei koyama;
                    {

                        itm.item.prefab.GetComponent<Armor>().UnUseArmor(GameManager.Instance.CurrentActor); //zirhi cikar
                        Debug.Log(itm.item.prefab.GetComponent<Armor>().gameObject);

                        dragableitem.item.prefab.UseItem(GameManager.Instance.CurrentActor); //yeni zirh tak
                        GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

                        DragDropManager tmpıtem = dragableitem;
                        dragableitem = itm;
                  
                        itm.parentAfterDrag = transform;
                        if (transform.childCount > 0)
                            transform.GetChild(0).SetParent(itm.oldParent);
                        itm.oldParent.GetComponent<ShieldSlot>().dragableitem = tmpıtem;


                        //yer degistirme



                    }

                }

            }
        }
        else if (itm.oldParent.GetComponent<HeadSlot>()) //head slottan geldimi
        {
            if (eventData.pointerDrag != null)
            {
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                if (transform.childCount == 0) //el bossa
                {
                    itm.oldParent.GetComponent<HeadSlot>().dragableitem = null;
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;


                }
                else
                {
                    if (dragableitem.item.type == ItemTpe.Armor && dragableitem.item.prefab.GetComponent<Armor>().armorType == Armor.ArmorType.Head) //ustundeki weoponsa yerdegis deilse gei koyama;
                    {
                        dragableitem.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                        GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

                        dragableitem = itm;
                        itm.parentAfterDrag = transform;
                        if (transform.childCount > 0)
                            transform.GetChild(0).SetParent(itm.oldParent);

                        //yer degistirme

                    }

                }

            }
        }
        else if (itm.oldParent.GetComponent<ChestSlot>()) //chest slottan geldimi
        {
            if (eventData.pointerDrag != null)
            {
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                if (transform.childCount == 0) //el bossa
                {
                    itm.oldParent.GetComponent<ChestSlot>().dragableitem = null;
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;


                }
                else
                {
                    if (dragableitem.item.type == ItemTpe.Armor && dragableitem.item.prefab.GetComponent<Armor>().armorType == Armor.ArmorType.Chest) //ustundeki weoponsa yerdegis deilse gei koyama;
                    {
                        dragableitem.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                        GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

                        dragableitem = itm;
                        itm.parentAfterDrag = transform;
                        if (transform.childCount > 0)
                            transform.GetChild(0).SetParent(itm.oldParent);

                        //yer degistirme

                    }

                }

            }
        }
        else if (itm.oldParent.GetComponent<FootSlot>()) //chest slottan geldimi
        {
            if (eventData.pointerDrag != null)
            {
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                if (transform.childCount == 0) //el bossa
                {
                    itm.oldParent.GetComponent<FootSlot>().dragableitem = null;
                    dragableitem = itm;
                    itm.parentAfterDrag = transform;


                }
                else
                {
                    if (dragableitem.item.type == ItemTpe.Armor && dragableitem.item.prefab.GetComponent<Armor>().armorType == Armor.ArmorType.Foot) //ustundeki weoponsa yerdegis deilse gei koyama;
                    {
                        dragableitem.item.prefab.UseItem(GameManager.Instance.CurrentActor);
                        GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

                        dragableitem = itm;
                        itm.parentAfterDrag = transform;
                        if (transform.childCount > 0)
                            transform.GetChild(0).SetParent(itm.oldParent);

                        //yer degistirme

                    }

                }

            }
        }
        /*
        else
        {
            if (eventData.pointerDrag != null)
            {   
               
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
                else if(itm.item.type == ItemTpe.Armor)
                {

                }

              

            }

           
        }
        */

    }
}
