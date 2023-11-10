using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    const string Farm = "Farming";

    public Nodes nextNode;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ally>())
        {
            if (other.GetComponent<Ally>().currentState == ActorState.Farming)
            {
                
                StartCoroutine(ChangeTarget(other.GetComponent<Ally>()));
            }
       
        }
    }

    IEnumerator ChangeTarget(Ally act)
    {
        if(act.currentState == ActorState.Farming)
        {
            act.animator.Play(Farm);

            act.actorBusy = true;
            yield return new WaitForSeconds(10);

            
            act.actorBusy = false;
            GameManager.Instance.Food += GameManager.Instance.farm.collectAmount*act.FarmSpeed;
            GameManager.Instance.farm.nodes = nextNode;
        }
         

    }
}
