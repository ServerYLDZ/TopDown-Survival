using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class AI_AttackState : AI_State
{
    public AI_State followState;
    public AI_State followAttackState;
    const string ATTACK = "Attack";
    const string ATTACK_SWORD = "SwordSlash";
    const string ATTACK_GUN = "GunShot";
    private Ally thisActor;
    public override AI_State RunState(Ally actor)
    {
       thisActor = actor;


    

        if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target)
        {
            if(actor.target)
            if (!actor.target.GetComponent<Enemy>())
            {
                actor.actorBusy = false;
                actor.currentState = ActorState.Follow;
                actor.target = null;
                    actor.Bar.DOFade(0, 1);
                    return followState;
            }
            else
            {
                float dis = actor.weapon.attackDistance; ;
                if (GameManager.Instance.playerActor.GetComponent<PlayerController>().target)
                    if (Vector3.Distance(actor.target.transform.position, actor.transform.position) >= dis)
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
            actor.Bar.DOFade(0, 1);
            return followState;
        }
        if (thisActor.target)
        {
            Vector3 dir = (thisActor.target.transform.position - thisActor.transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRot, Time.deltaTime * thisActor.lookRotationSpeed);
            actor.Bar.transform.LookAt(Camera.main.transform);
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
        }
        else
        {
            actor.actorBusy = false;
            actor.target = GameManager.Instance.playerActor.GetComponent<PlayerController>().target.transform;
            actor.currentState = ActorState.Attack;
            return followAttackState;
        }
      
        return this;
    }

    

    AI_State SendAttack()
    {

        if (thisActor.target == null)
        {
            thisActor.actorBusy = false;
            thisActor.target = null;
            thisActor.currentState = ActorState.Follow;
            thisActor.Bar.DOFade(0, 1);
            return followState;
        }
        if(thisActor.target.GetComponent<Enemy>())
        if (thisActor.target.GetComponent<Enemy>().currentHealth <= 0)
        {
        thisActor.currentState = ActorState.Follow;
            thisActor.target = null; 
        }

       
        if(thisActor.target && thisActor&& thisActor.target.GetComponent<Enemy>())
        {
            
            Instantiate(thisActor.hitEffect, thisActor.target.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            thisActor.target.GetComponent<Enemy>().TakeDamage(thisActor.weapon.attackDamage);
            if (thisActor.target.GetComponent<Enemy>().currentHealth <= 0) { //hasardan Sonra oldurmussem xp Kazanirim;
                thisActor.TakeXP(thisActor.target.GetComponent<Enemy>().amoutXP);
            }
        }
      

        return this;
    }


    void ResetBusyState()
    {
        if (transform.parent.GetComponent<Actor>().currentHealth > 0)
        {
            transform.parent.GetComponent<Actor>().actorBusy = false;
            thisActor.SetAnimations();
        }
    
    }
}
