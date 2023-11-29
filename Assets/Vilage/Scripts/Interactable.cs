using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType { Enemy, Item,Ally,AsignedOnject }
public enum AsignedObjectType {None, Farm,Wood}
public class Interactable : MonoBehaviour
{

    public Actor myEnemy { get; private set; }
    public Ally myAlly { get; private set; }

    public InteractableType interactionType;
    public AsignedObjectType asignedObjectType;
    void Awake()
    {
        if (interactionType == InteractableType.Enemy)
        { myEnemy = GetComponent<Enemy>(); }
        if(interactionType == InteractableType.Ally)
        {
            myAlly = GetComponent<Ally>();
        }
    }

    public void InteractWithItem()
    {
       
            if (GetComponent<ItemBase>().item.type == ItemTpe.Weapon)
            {

                if (GameManager.Instance.Inventer.AddInventory(GetComponent<ItemBase>().item))
                {

                    Destroy(gameObject);
                }

            }
            if (GetComponent<ItemBase>().item.type == ItemTpe.Armor)
            {

                if (GameManager.Instance.Inventer.AddInventory(GetComponent<ItemBase>().item))
                {
                    Debug.Log("hi");
                    Destroy(gameObject);
                }

            }
            if (GetComponent<ItemBase>().item.type == ItemTpe.Food)
            {
                if (GameManager.Instance.Inventer.AddInventory(GetComponent<ItemBase>().item))
                {
                    Destroy(gameObject);
                }
            }
            if (GetComponent<ItemBase>().item.type == ItemTpe.Wood)
            {
                if (GameManager.Instance.Inventer.AddInventory(GetComponent<ItemBase>().item))
                {
                    Destroy(gameObject);
                }
            }
        
     

    }
    public void InteractWithAlly()
    {
        if (myAlly.currentState == ActorState.None)
        {
            
            myAlly.currentState = ActorState.Follow;
            myAlly.actorControlItem = GameManager.Instance.ActorControler.AddActorControlPrefab(myAlly);
            GameManager.Instance.peapleCount++;
            HUD.Instance.ResourcesSet();
           // ActorControlPanel.Instance.AddActorControlPrefab(myAlly);
         
            //effect girer 
        }
        else
        {
            myAlly.InfoPanel.OpenCloseInfoPanel();
            GameManager.Instance.OpenInventor();
        }
     

    }
}
