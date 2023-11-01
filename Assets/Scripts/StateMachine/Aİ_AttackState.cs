using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aİ_AttackState : AI_State
{
    public AI_State followState;
    public AI_State followAttackState;
    const string ATTACK = "Attack";
    const string ATTACK_SWORD = "SwordSlash";
    const string ATTACK_GUN = "GunShot";
    private Actor thisActor;
    public override AI_State RunState(Actor actor)
    {
       thisActor = actor;

 


        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target)
        {
            if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target.interactionType != InteractableType.Enemy)
            {
                actor.actorBusy = false;
                actor.currentState = ActorState.Follow;
                actor.target = null;
                return followState;
            }
            else
            {
                float dis = actor.weapon.attackDistance; ;
                if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target)
                    if (Vector3.Distance(GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform.position, actor.transform.position) >= dis)
                    {
                        actor.actorBusy = false;
                        actor.target = GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform;
                        actor.currentState = ActorState.Attack;
                        return followAttackState;
                    }
            }
                
        }
        else
        {
            actor.actorBusy = false;
            actor.target = null;
            actor.currentState = ActorState.Follow;
            return followState;
        }


        Vector3 dir = (thisActor.target.transform.position - thisActor.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRot, Time.deltaTime * thisActor.lookRotationSpeed);


        thisActor.agent.SetDestination(actor.transform.position); //durdur






        if (thisActor.actorBusy) return this;

        thisActor.actorBusy = true;

                switch (thisActor.weapon.weponType) //sliah tipine gore anim oynar
                {
                    case Weapon.WeaponType.None:
                       thisActor.animator.Play(ATTACK);
                        break;
                    case Weapon.WeaponType.Sword:
                thisActor.animator.Play(ATTACK_SWORD);
                        break;
                    case Weapon.WeaponType.Gun:
                thisActor.animator.Play(ATTACK_GUN);
                        break;
                }

       

        Invoke("SendAttack", thisActor.weapon.attackDelay);


         Invoke("ResetBusyState", thisActor.weapon.attackSpeed);

        return this;
    }

    

    AI_State SendAttack()
    {

        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target == null)
        {
            thisActor.actorBusy = false;
            thisActor.target = null;
            thisActor.currentState = ActorState.Follow;
            return followState;
        }
        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target.myActor.currentHealth <= 0)
        {
        thisActor.currentState = ActorState.Follow;
            GameManager.Instance.playerActor.GetComponent<PlayerController>().target = null; 
        }

       

        Instantiate(thisActor.hitEffect, GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        GameManager.Instance.playerActor.GetComponent<PlayerController>().target.GetComponent<Actor>().TakeDamage(thisActor.weapon.attackDamage);

        return this;
    }


    void ResetBusyState()
    {
        Debug.Log("sıfırla");
        transform.parent.GetComponent<Actor>().actorBusy = false;
        thisActor.SetAnimations();
    }
}
