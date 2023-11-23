using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class FarmAssignPanel : MonoBehaviour, IDropHandler
{
    public AssigmentUI actorFarmerPrefab;
    public Transform spawnPoint;
    public TMP_Text informaionText;
    public TMP_Text limitsText;

    private void OnEnable()
    {
        limitsText.text = GameManager.Instance.farm.currentWorkerCount + " / " + GameManager.Instance.farm.MaxFarmWorker;
        if (transform.childCount <= 0)
        {
            informaionText.text = "Please drop ally here for assign...";
        }
    }
 
    public void AddActorFarmerPrefab(Ally act)
    {
        AssigmentUI prfb = Instantiate(actorFarmerPrefab, spawnPoint);
        prfb.image.sprite = act.ClassSprite;
        prfb.Name.text = act.ActName.ToString();
        prfb.jobText.text = act.FarmSpeed.ToString();
        prfb.ally = act;

    }


 
    public void OnDrop(PointerEventData eventData)
    {
        if (GameManager.Instance.farm.MaxFarmWorker > GameManager.Instance.farm.currentWorkerCount)
        {
            ActorControlItem itm = eventData.pointerDrag.GetComponent<ActorControlItem>();
            if (itm)
            {
                if (itm.ally.myClass != ActorClass.Leader)
                {
                    itm.ally.currentState = ActorState.Farming;
                    AddActorFarmerPrefab(itm.ally);
                    GameManager.Instance.isActorFolowPlayerPosUseForNow[itm.ally.AllyIndex]= false;
                    Destroy(itm.gameObject);
                    GameManager.Instance.farm.currentWorkerCount++;
                    informaionText.text = "";
                    limitsText.text = GameManager.Instance.farm.currentWorkerCount + " / " + GameManager.Instance.farm.MaxFarmWorker;
                }
                else
                {
                    Debug.Log("Leader cant Asigment This job");
                }

            }
        }
        else
        {
            Debug.Log("Farm is Full for Assigment");
        }
    }
}
