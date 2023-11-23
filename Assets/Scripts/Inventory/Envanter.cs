using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum ItemTpe
{
    Food,
    Wood,
    Watter,
    Item,
    Weapon,
    Armor
}
public class Envanter : MonoBehaviour
{
   

   public List<ItemSlot> InventorySlots;
    public DragDropManager ItemUI;
   
    
    public bool AddInventory(Item item)
    {
        

        if (item.stackable)
        {
            for (int i = 0; i < InventorySlots.Count; i++)//eğer staklenebiliyorsa ekleme sayısını arttır
            {
                if(InventorySlots[i].dragableitem)
                if (InventorySlots[i].dragableitem.item.Name == item.Name)
                {   
                    InventorySlots[i].dragableitem.amount += item.count;
                   
                    return true;
                }
              
            }

            foreach (var slot in InventorySlots)
            {
                if (slot.dragableitem==null)
                {
                    DragDropManager dragableItem = Instantiate(ItemUI, slot.transform);
                 
                    dragableItem.amount+= item.count;
                    dragableItem.item = item;
                    slot.dragableitem = dragableItem;
            
                    
                   
                    return true;
                }
            }

        }

      

            if (RetrunEmptySlot())
            {
                
                foreach (var slot in InventorySlots)
                {
                    if (slot.dragableitem==null)
                    {
                    DragDropManager dragableItem = Instantiate(ItemUI, slot.transform);
                    
                  
                    dragableItem.amount += item.count;
                    dragableItem.item = item;
                    slot.dragableitem = dragableItem;
                    
                   

                    return true;
                    }
                 
                }
            }
            else
            {
                Debug.Log("Inventory is Full");
            return false;
            }



        return false;
       
    }
    public ItemSlot RetrunEmptySlot()
    {
        foreach (var slot in InventorySlots)
        {
            if (slot.dragableitem == null)
            {
                return slot;
            }
        }
        return null;
    }
}
