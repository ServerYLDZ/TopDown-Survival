using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    const string Farm = "Farming";
    const string Wood = "Cutting";

    public Nodes nextNode;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ally>())
        {
            if (other.GetComponent<Ally>().currentState == ActorState.Farming)
            {
                StartCoroutine(ChangeTarget(other.GetComponent<Ally>()));
            }
            else if (other.GetComponent<Ally>().currentState == ActorState.Cuting)
            {
                if (this== GameManager.Instance.woodTree.FirstNode )
                {
                   
                    StartCoroutine(ChangeTarget(other.GetComponent<Ally>()));
                }
                else
                {
                    GameManager.Instance.woodTree.nodes = nextNode;
                }
              
            }
            else
            {
                StopAllCoroutines();
                other.GetComponent<Ally>().FollowPlayer();
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
        else if (act.currentState == ActorState.Cuting)
        {
            act.animator.Play(Wood);

            act.actorBusy = true;
            yield return new WaitForSeconds(10);

            act.actorBusy = false;
            GameManager.Instance.Wood += GameManager.Instance.woodTree.collectAmount * act.WoodSpeed;
            GameManager.Instance.woodTree.nodes = nextNode;
        }


    }
}
