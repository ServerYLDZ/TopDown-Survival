using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class DragDropManager : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler,IPointerClickHandler
{
    [SerializeField] Canvas canvas;
 
    private CanvasGroup cncsGroup;
    public Transform parentAfterDrag;
    public Transform oldParent;
 
    public Item item;
    public Image image;
    public TMP_Text amountTexxt;
    public int amount = 0;


    private void OnEnable()
    {
        image.sprite =item.image;
          
          if (item.type == ItemTpe.Weapon)
            {
                amountTexxt.text = "";
                
            }
            else
            {
                amountTexxt.text = amount.ToString();
            }
           
        

    }
    private void Awake()
    {
      
        cncsGroup = GetComponent<CanvasGroup>();
    }
   
    public void OnPointerClick(PointerEventData eventData)
    {
        
       if (eventData.clickCount == 2 && item.name != "Hand")
       {

           //use
           if (transform.parent.GetComponent<ItemSlot>())
           {
               if (item.type == ItemTpe.Weapon)
               {

                   if (GameManager.Instance.CurrentActor.weponSlot.dragableitem == null) //el bossa
                   {

                       transform.parent.GetComponent<ItemSlot>().dragableitem = null;
                       item.prefab.UseItem(GameManager.Instance.CurrentActor);
                       transform.SetParent(GameManager.Instance.CurrentActor.weponSlot.transform);
                       transform.parent.GetComponent<WeaponSlot>().dragableitem = this;
                       GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                   }
                   else//el doluysa yer degis
                   {
                       transform.parent.GetComponent<ItemSlot>().dragableitem = GameManager.Instance.CurrentActor.weponSlot.dragableitem;
                       item.prefab.UseItem(GameManager.Instance.CurrentActor);
                       GameManager.Instance.CurrentActor.weponSlot.transform.GetChild(0).transform.SetParent(transform.parent);
                       transform.SetParent(GameManager.Instance.CurrentActor.weponSlot.transform);
                       GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                   }


               }
               else if (item.type == ItemTpe.Armor) // tpine gore armor giy
               {

                    switch (item.prefab.GetComponent<Armor>().armorType)
                   {
                       case Armor.ArmorType.Shield:

                           if (GameManager.Instance.CurrentActor.slieldSlot.dragableitem == null) //el bossa
                           {

                               transform.parent.GetComponent<ItemSlot>().dragableitem = null;
                               item.prefab.UseItem(GameManager.Instance.CurrentActor);
                               transform.SetParent(GameManager.Instance.CurrentActor.slieldSlot.transform);
                               transform.parent.GetComponent<ShieldSlot>().dragableitem = this;
                               GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                           }
                           else//el doluysa yer degis
                           {

                               GameManager.Instance.CurrentActor.slieldSlot.dragableitem.item.prefab.GetComponent<Armor>().UnUseArmor(GameManager.Instance.CurrentActor); //zirhi cikar eskisini
                                Debug.Log(GameManager.Instance.CurrentActor.slieldSlot.dragableitem.item.prefab);
                               transform.parent.GetComponent<ItemSlot>().dragableitem = GameManager.Instance.CurrentActor.slieldSlot.dragableitem;
                               item.prefab.UseItem(GameManager.Instance.CurrentActor);
                               GameManager.Instance.CurrentActor.slieldSlot.transform.GetChild(0).transform.SetParent(transform.parent);
                               transform.SetParent(GameManager.Instance.CurrentActor.slieldSlot.transform);
                                GameManager.Instance.CurrentActor.slieldSlot.dragableitem = this;
                               GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
                           }

                           break;
                       case Armor.ArmorType.Head:
                           break;
                       case Armor.ArmorType.Chest:
                           break;
                       case Armor.ArmorType.Foot:
                           break;
                       default:
                           break;
                   }

               }
               else if (item.type == ItemTpe.Food)
               {
                   item.prefab.UseItem(GameManager.Instance.CurrentActor);
                   amount--;
                   amountTexxt.text = amount.ToString();
                   transform.parent.GetComponent<ItemSlot>().dragableitem = null;
                   if (amount <= 0)
                       Destroy(this.gameObject);

                   GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
               }
               else if (item.type == ItemTpe.Wood)
               {
                   item.prefab.UseItem(GameManager.Instance.CurrentActor);
                   amount--;
                   amountTexxt.text = amount.ToString();
                   transform.parent.GetComponent<ItemSlot>().dragableitem = null;
                   if (amount <= 0)
                       Destroy(this.gameObject);

                   GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
               }
               //diger nesnelerle etkilesim burdan kontrol edilcek
           }
           else if(transform.parent.GetComponent<WeaponSlot>())
           {
               if (item.type == ItemTpe.Weapon)
               {
                   transform.parent.GetComponent<WeaponSlot>().dragableitem = null;

                   GameManager.Instance.CurrentActor.weapon = GameManager.Instance.HandWepon;
                   if (GameManager.Instance.CurrentActor.weponTransform.childCount > 0)
                       Destroy(GameManager.Instance.CurrentActor.weponTransform.GetChild(0).gameObject);
                   transform.SetParent(GameManager.Instance.Inventer.RetrunEmptySlot().transform);
                   GameManager.Instance.Inventer.RetrunEmptySlot().dragableitem = this;

                   GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

               }

           }
           else if (transform.parent.GetComponent<ShieldSlot>()) //----------------------------------------------------------------
           {
               if (item.type == ItemTpe.Armor && item.prefab.GetComponent<Armor>().armorType==Armor.ArmorType.Shield)
               {
                  this.item.prefab.GetComponent<Armor>().UnUseArmor(GameManager.Instance.CurrentActor); //zirhi cikar eskisini
                    Debug.Log(this.item.prefab);
                    transform.parent.GetComponent<ShieldSlot>().dragableitem = null;
                 
                   //zirhi cikar kodunu yaz   ve kalkan yok et

                   transform.SetParent(GameManager.Instance.Inventer.RetrunEmptySlot().transform);
                   GameManager.Instance.Inventer.RetrunEmptySlot().dragableitem = this;
                   GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

               }

           }
           else if (transform.parent.GetComponent<HeadSlot>()) //----------------------------------------------------------------
           {
               if (item.type == ItemTpe.Armor && item.prefab.GetComponent<Armor>().armorType == Armor.ArmorType.Head)
               {
                   transform.parent.GetComponent<HeadSlot>().dragableitem = null;

                   //zirhi cikar kodunu yaz   ve kalkan yok et
                     if (GameManager.Instance.CurrentActor.weponTransform.childCount > 0)
                         Destroy(GameManager.Instance.CurrentActor.weponTransform.GetChild(0).gameObject);
                   transform.SetParent(GameManager.Instance.Inventer.RetrunEmptySlot().transform);
                   GameManager.Instance.Inventer.RetrunEmptySlot().dragableitem = this;
                   GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

               }

           }
           else if (transform.parent.GetComponent<ChestSlot>()) //----------------------------------------------------------------
           {
               if (item.type == ItemTpe.Armor && item.prefab.GetComponent<Armor>().armorType == Armor.ArmorType.Chest)
               {
                   transform.parent.GetComponent<ChestSlot>().dragableitem = null;

                   //zirhi cikar kodunu yaz   ve kalkan yok et
                    if (GameManager.Instance.CurrentActor.weponTransform.childCount > 0)
                         Destroy(GameManager.Instance.CurrentActor.weponTransform.GetChild(0).gameObject);
                   transform.SetParent(GameManager.Instance.Inventer.RetrunEmptySlot().transform);
                   GameManager.Instance.Inventer.RetrunEmptySlot().dragableitem = this;
                   GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

               }

           }
           else if (transform.parent.GetComponent<FootSlot>()) //----------------------------------------------------------------
           {
               if (item.type == ItemTpe.Armor && item.prefab.GetComponent<Armor>().armorType == Armor.ArmorType.Foot)
               {
                   transform.parent.GetComponent<FootSlot>().dragableitem = null;

                   //zirhi cikar kodunu yaz   ve kalkan yok et
                    if (GameManager.Instance.CurrentActor.weponTransform.childCount > 0)
                         Destroy(GameManager.Instance.CurrentActor.weponTransform.GetChild(0).gameObject);
                   transform.SetParent(GameManager.Instance.Inventer.RetrunEmptySlot().transform);
                   GameManager.Instance.Inventer.RetrunEmptySlot().dragableitem = this;
                   GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();

               }

           }


       }
           
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.GetComponent<WeaponSlot>())
        {
         
          
            if (GameManager.Instance.CurrentActor.weponTransform.childCount > 0)
             Destroy(GameManager.Instance.CurrentActor.weponTransform.GetChild(0).gameObject);
         

        }


        if (item.name != "Hand")
        {
            oldParent = transform.parent;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            cncsGroup.blocksRaycasts = false;
            cncsGroup.alpha = .6f;
        }
       
    
        
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (item.name != "Hand")
            transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (item.name != "Hand")
        {
            transform.SetParent(parentAfterDrag);
            cncsGroup.blocksRaycasts = true;
            cncsGroup.alpha = 1;
            
        }

        StartCoroutine(FixWeapon());
     




    }
    

    IEnumerator FixWeapon()
    {
        yield return new WaitForSeconds(.3f);
        
        if ( oldParent.GetComponent<WeaponSlot>() &&  parentAfterDrag.GetComponent<ItemSlot>()) //çektiğimyer wepon slot ve koydugum envanter slotsa
        {
            if (oldParent.childCount == 0)
            {
                oldParent.GetComponent<WeaponSlot>().dragableitem = null;
                parentAfterDrag.GetComponent<ItemSlot>().dragableitem = this;
                GameManager.Instance.HandWepon.UseItem(GameManager.Instance.CurrentActor);
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
            }//elim bosaltilir
               
        }
        else if (oldParent.GetComponent<ItemSlot>() && parentAfterDrag.GetComponent<WeaponSlot>()) //envanter cekip slota koyma
        {
            oldParent.GetComponent<ItemSlot>().dragableitem = null;
            parentAfterDrag.GetComponent<WeaponSlot>().dragableitem = this;
            item.prefab.UseItem(GameManager.Instance.CurrentActor);
            GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
        }
        else if (parentAfterDrag == oldParent && oldParent.GetComponent<WeaponSlot>()) //drag iptalse  ve silah kısmıysa kullan tekrar ayni silahi
        {   

            item.prefab.UseItem(GameManager.Instance.CurrentActor);
            GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
        }
       
    }
  


}
