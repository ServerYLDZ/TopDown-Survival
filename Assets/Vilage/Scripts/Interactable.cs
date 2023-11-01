using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType { Enemy, Item,Ally }
public class Interactable : MonoBehaviour
{

    public Actor myActor { get; private set; }
    public Ally myAlly { get; private set; }

    public InteractableType interactionType;

    void Awake()
    {
        if (interactionType == InteractableType.Enemy)
        { myActor = GetComponent<Actor>(); }
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
     if(GetComponent<ItemBase>().item.type == ItemTpe.Food)
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
            Debug.Log("Hi");
            myAlly.currentState = ActorState.Follow;
            ActorControlPanel.Instance.AddActorControlPrefab(myAlly);
         
            //effect girer 
        }
        else
        {
            myAlly.InfoPanel.OpenCloseInfoPanel();
            GameManager.Instance.OpenInventor();
        }
     

    }
}
