using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float Food;
    public float Wood;
    public float Watter;
    public Actor playerActor;
    public Actor CurrentActor;
    public Envanter Inventer;
    public Weapon HandWepon;
    public GameObject InventoryTAB;
    private bool inventortyOpen = false;
    public Actor[] allActors;
    public Transform []actorFolowPlayerPos;
    public bool[] isActorFolowPlayerPosUseForNow;

    public void OpenInventor()
    {
        InventoryTAB.SetActive(true);
        inventortyOpen = !inventortyOpen;
    }
    public void CloseInventor()
    {
        InventoryTAB.SetActive(false);
        inventortyOpen = !inventortyOpen;
        foreach (var actor in allActors) //tum diger panellerikapa
        {
            actor.InfoPanel.isOpen = false;
            actor.InfoPanel.gameObject.SetActive(false);
        }
        GameManager.Instance.CurrentActor = GameManager.Instance.playerActor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventortyOpen)
                CloseInventor();  
            else
                OpenInventor();
      
          
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
