using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType { Enemy, Item,Ally }
public class Interactable : MonoBehaviour
{

    public Actor myActor { get; private set; }

    public InteractableType interactionType;

    void Awake()
    {
        if (interactionType == InteractableType.Enemy || interactionType == InteractableType.Ally)
        { myActor = GetComponent<Actor>(); }
       
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
        if (myActor.currentState == ActorState.None)
        {
            Debug.Log("Hi");
            myActor.currentState = ActorState.Follow;
            ActorControlPanel.Instance.AddActorControlPrefab(myActor);
         
            //effect girer 
        }
        else
        {
            myActor.InfoPanel.OpenCloseInfoPanel();
            GameManager.Instance.OpenInventor();
        }
     

    }
}
