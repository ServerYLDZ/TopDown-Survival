using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class DragDropManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler,IEndDragHandler
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
    public void OnPointerDown(PointerEventData eventData) //kullan ve cikar
    {
        
        if (eventData.clickCount >= 2 &&  item.name != "Hand")
        {
            //use
            if (transform.parent.GetComponent<ItemSlot>().slotType== ItemSlotType.EnvanterSlot)
            {
                if (item.type == ItemTpe.Weapon)
                {
                    if (GameManager.Instance.CurrentActor.weponSlot.dragableitem == null) //el bossa
                    {
                        transform.parent.GetComponent<ItemSlot>().dragableitem = null;
                        item.prefab.UseItem(GameManager.Instance.CurrentActor);
                        transform.SetParent(GameManager.Instance.CurrentActor.weponSlot.transform);
                        transform.parent.GetComponent<ItemSlot>().dragableitem = this;
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
            else
            {
                if (item.type == ItemTpe.Weapon)
                {
                    transform.parent.GetComponent<ItemSlot>().dragableitem = null;

                    GameManager.Instance.CurrentActor.weapon = GameManager.Instance.HandWepon;
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
        if (transform.parent.GetComponent<ItemSlot>().slotType == ItemSlotType.WeaponSlot)
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

        if (ItemSlotType.WeaponSlot == oldParent.GetComponent<ItemSlot>().slotType && ItemSlotType.EnvanterSlot == parentAfterDrag.GetComponent<ItemSlot>().slotType) //çektiğimyer wepon slot ve koydugum envanter slotsa
        {
            if (oldParent.childCount == 0)
            {
                oldParent.GetComponent<ItemSlot>().dragableitem = null;
                parentAfterDrag.GetComponent<ItemSlot>().dragableitem = this;
                GameManager.Instance.HandWepon.UseItem(GameManager.Instance.CurrentActor);
                GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
            }//elim bosaltilir
               
        }
        else if (ItemSlotType.EnvanterSlot == oldParent.GetComponent<ItemSlot>().slotType && ItemSlotType.WeaponSlot == parentAfterDrag.GetComponent<ItemSlot>().slotType) //envanter cekip slota koyma
        {
            oldParent.GetComponent<ItemSlot>().dragableitem = null;
            parentAfterDrag.GetComponent<ItemSlot>().dragableitem = this;
            item.prefab.UseItem(GameManager.Instance.CurrentActor);
            GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
        }
        else if (parentAfterDrag == oldParent && oldParent.GetComponent<ItemSlot>().slotType== ItemSlotType.WeaponSlot ) //drag iptalse  ve silah kısmıysa kullan tekrar ayni silahi
        {   

            item.prefab.UseItem(GameManager.Instance.CurrentActor);
            GameManager.Instance.CurrentActor.InfoPanel.SetInfoPanel();
        }
       
    }
  
   
}
