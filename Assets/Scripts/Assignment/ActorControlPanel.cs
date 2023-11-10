using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ActorControlPanel : MonoSingleton<ActorControlPanel> ,IDropHandler
{
    public ActorControlItem actorControlPrefab;
    public Transform spawnPoint;
  
    public ActorControlItem AddActorControlPrefab(Ally act)
    {
        ActorControlItem prfb = Instantiate(actorControlPrefab, spawnPoint);
        prfb.image.sprite = act.ClassSprite;
        prfb.Name.text = act.ActName.ToString();
        prfb.Healt.text = act.currentHealth.ToString();
        prfb.Hunger.text = act.Hunger.ToString();
        prfb.currentState.text = act.currentState.ToString();
        prfb.ally = act;
        return prfb;
    }

   

    public void RefreshActorControlPrefab(Ally act , ActorControlItem item)
    {
        item.image.sprite = act.ClassSprite;
        item.Name.text = act.ActName.ToString();
        item.Healt.text = act.currentHealth.ToString();
        item.Hunger.text = act.Hunger.ToString();
        item.currentState.text = act.currentState.ToString();
        item.ally = act;
    }
    public void OnDrop(PointerEventData eventData)
    {
      
       AssigmentUI itm = eventData.pointerDrag.GetComponent<AssigmentUI>();
        if (itm)
        {
            if (itm.ally.currentState == ActorState.Farming) // diger atamalarda burasina yeni eklemeler yapilcak if wooding gibi
            {
                GameManager.Instance.farm.currentWorkerCount--;
                //text deisimleri icin alt kisim var
              itm.oldParent.GetComponent<FarmAssignPanel>().limitsText.text = GameManager.Instance.farm.currentWorkerCount + " / " + GameManager.Instance.farm.MaxFarmWorker;
                if (itm.oldParent.childCount <= 0)
                {
                    itm.oldParent.GetComponent<FarmAssignPanel>().informaionText.text = "Please drop ally here for assign...";
                }
            }

            itm.ally.currentState = ActorState.Follow;
            AddActorControlPrefab(itm.ally);         
            Destroy(itm.gameObject);
        }
           
    }
}
