using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    
    public float Food;
    public float Wood;
    public float Watter;
    public Ally playerActor;
    public Ally CurrentActor;
    public ActorControlPanel ActorControler;
    public Envanter Inventer;
    public Weapon HandWepon;
    public GameObject InventoryTAB;
    public GameObject ActorControlPanelTAB;
    public GameObject FarmAssignPanelTAB;
    private bool isOpenAnnyPanelNow = false;
    private bool inventortyOpen = false;
    private bool actorControlPanelOpen = false;
    public Ally[] allActors;
    public Transform []actorFolowPlayerPos;
    public bool[] isActorFolowPlayerPosUseForNow;
    public Farm farm;
    public bool isGameOver = false;
    public void OpenInventor()
    {
        if (!isOpenAnnyPanelNow)
        {
            InventoryTAB.SetActive(true);
            inventortyOpen = !inventortyOpen;
            isOpenAnnyPanelNow = true;
        }
      
    }
    public void CloseInventor()
    {
        InventoryTAB.SetActive(false);

        if (InventoryTAB.GetComponentInChildren<ItemPopUp>())
        {
            
            InventoryTAB.GetComponentInChildren<ItemPopUp>().gameObject.SetActive(false); //pop up kapama
         
        }
  
        inventortyOpen = !inventortyOpen;
        isOpenAnnyPanelNow = false;
        foreach (var actor in allActors) //tum diger panellerikapa
        {
            actor.InfoPanel.isOpen = false;
            actor.InfoPanel.gameObject.SetActive(false);
        }
        GameManager.Instance.CurrentActor = GameManager.Instance.playerActor;
    }
    public void OpenActorControlPanel()
    {
        if (!isOpenAnnyPanelNow)
        {
            ActorControlPanelTAB.SetActive(true);
            actorControlPanelOpen = true;
            isOpenAnnyPanelNow = true;
          
        }
     
    }
    public void CloseActorControlPanel()
    {
        ActorControlPanelTAB.SetActive(false);
        actorControlPanelOpen = false;
        isOpenAnnyPanelNow = false;
    }
    public void OpenFarmAssignPanel()
    {
        if (!isOpenAnnyPanelNow)
        {
            ActorControlPanelTAB.SetActive(true);
            FarmAssignPanelTAB.SetActive(true);
            actorControlPanelOpen = true;
            isOpenAnnyPanelNow = true;
        }
       
    }
    public void CloseFarmAssignPanel()
    {
        ActorControlPanelTAB.SetActive(false);
        FarmAssignPanelTAB.SetActive(false);
        actorControlPanelOpen = false;
        isOpenAnnyPanelNow = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) )
        {
            
            if (!inventortyOpen )
                OpenInventor();
            else
                CloseInventor();

        }
        if (Input.GetKeyDown(KeyCode.P) )
        {
            
            if (!actorControlPanelOpen)
            {
                OpenActorControlPanel();
                OpenFarmAssignPanel();
            }
            else
            {
                CloseActorControlPanel();
                CloseFarmAssignPanel();
               
            }
        }
    }

    public void IsActorFolowPlayerPosUseForNow()
    {
        for (int i = 0; i < isActorFolowPlayerPosUseForNow.Length; i++)
        {
            isActorFolowPlayerPosUseForNow[i] = false;
        }
    }

}
