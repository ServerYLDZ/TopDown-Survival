using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Actor
{
    private bool isTrigered;
    public LayerMask targetLayer;
    public List<Collider> targets;
    public float attackDis;
    public float attackDelay=.3f;
    public float attackSpeed=2;
    public int attackDamage=1;
    public ParticleSystem hitEffect;
    private AnimationsController animController;
    public Enemy[] enemyAllys;
    private Vector3 StartPoint;
    public float followMaxRange = 10;
    private bool getingHit;
    private void Start()
    {
        animController = GetComponent<AnimationsController>();
        StartPoint = transform.position;
    }
    public void AttackModeOn()
    {
        SetTargetsList();
        if (targets.Count > 0)
        {
            int rand = Random.Range(0, targets.Count);

            target = targets[rand].transform;
            FollowTarget();
        }
        else
        {
            target = null;
            targets.RemoveRange(0, targets.Count);
            agent.SetDestination(StartPoint);
        }

    }

    void FollowTarget()
    {
       float dis;
        if (getingHit) return;
        if (target == null)
        {
            targets.RemoveRange(0, targets.Count);
            agent.SetDestination(StartPoint);

            return;
        }
        dis = attackDis;
       

        if (Vector3.Distance(target.transform.position, transform.position) <= dis)
        { ReachDistance();
            
         }
        else  if (Vector3.Distance(StartPoint, transform.position) >= followMaxRange)
        {
            agent.SetDestination(StartPoint);
            target = null;
            targets.RemoveRange(0, targets.Count);

        }
        else
        { 
                agent.SetDestination(target.transform.position);
        }
    }
    public void ReachDistance()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * lookRotationSpeed);


        agent.SetDestination(transform.position); //durdur






        if (actorBusy) return;
            actorBusy = true;

        //animasyon girer
        animController.Attack();


        Invoke("SendAttack", attackDelay);


        Invoke("ResetBusyState",attackSpeed);

    }

   void SendAttack()
    {

        if (target == null)
        {
            actorBusy = false;
            targets.RemoveRange(0, targets.Count);
            AttackModeOn();
           
          
            return;
          
        }
        if (target.GetComponent<Ally>().currentHealth <= 0)
        {
            actorBusy = false;
            target = null;
            targets.RemoveRange(0, targets.Count);
            AttackModeOn();
         
            return;
        }



        Instantiate(hitEffect, target.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        target.GetComponent<Ally>().TakeDamage(attackDamage);

        
    }


    void ResetBusyState()
    {

        actorBusy = false;
        SetAnimations();
    }
    public void SetTargetsList()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 10, targetLayer);
       
        foreach (var item in col)
        {
            if (item.GetComponent<Ally>())
            {
                targets.Add(item);
            }
        }
    }

    public override void TakeDamage(int amount)
    {
       
        currentHealth -= amount;
        isTrigered = true;
        if (!target)
        {
            AttackModeOn();
            foreach (var enemy in enemyAllys)
            {
                if(enemy)
                enemy.AttackModeOn();
               
            }
        }
        
        getingHit = true;
        agent.SetDestination(transform.position);
        animController.Hit();
        Invoke("ResetActorBustState", .5f);
        
        if (currentHealth <= 0)
        { Death(); }
    }
  public override void Death()
    {
        foreach (var enemy in enemyAllys)
        {
            if (enemy)
            {
                GameManager.Instance.playerActor.GetComponent<PlayerController>().target = enemy.transform.GetComponent<Interactable>();
            }
        }
        agent.SetDestination(transform.position);
        this.enabled=false;
        animController.SetDead();
        Destroy(gameObject,3);
    }

    public void SetAnimations()
    {
    
        if (actorBusy) return;
        if (getingHit) return;
        if (agent.velocity == Vector3.zero)
        { animController.SetMovingState(false); }
        else
        { animController.SetMovingState(true); }

    }
    void ResetActorBustState()
    {
       getingHit = false;
    }
    private void Update()
    {
       
        FollowTarget();
        SetAnimations();
    }

}
