using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
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
    public Transform BarCanvas;
    public RectTransform HealthBar;
    public Transform ArmorBar;
    public TMP_Text healtText;
    public TMP_Text armorText;
    private bool isDead;
    private void Start()
    {
        animController = GetComponent<AnimationsController>();
        StartPoint = transform.position;
        healtText.text = currentHealth.ToString();
        armorText.text =  armor.ToString();
        ArmorBarActive();
    }
    public void AttackModeOn()
    {
        SetTargetsList();
        if (targets.Count > 0)
        {
            int rand = Random.Range(0, targets.Count);
            BarCanvas.GetComponent<CanvasGroup>().DOFade(1, 1);
            target = targets[rand].transform;
            FollowTarget();
        }
        else
        {
            target = null;
            targets.RemoveRange(0, targets.Count);
            agent.SetDestination(StartPoint);
            BarCanvas.GetComponent<CanvasGroup>().DOFade(0, 1);
        }

    }

    void FollowTarget()
    {
       float dis;
        if (isDead) return;
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
            BarCanvas.GetComponent<CanvasGroup>().DOFade(0, 1);

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
        BarCanvas.LookAt(Camera.main.transform);

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
    public void ArmorBarActive()
    {
        if (armor > 0)
        {
            ArmorBar.gameObject.SetActive(true);
            for (int i = 0; i < ArmorBar.childCount; i++)
            {
                if (i <= armor)
                    ArmorBar.GetChild(i).gameObject.SetActive(true);
                else
                    ArmorBar.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            ArmorBar.gameObject.SetActive(false);
        }

    }

    public override void TakeDamage(int amount)
    {

        if (amount > armor)
        {
            currentHealth -= amount-armor;
            healtText.text = currentHealth.ToString();
            HealthBar.DOScaleX((float)currentHealth / (float)maxHealth, .5f);

        }
        else
        {
            currentHealth -= 0;
        }
      
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
       
            
            agent.SetDestination(transform.position);

          
        
      
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healtText.text = currentHealth.ToString();
            HealthBar.DOScaleX(0, .5f);
            Death();
        }
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
        isDead = true;
        agent.SetDestination(transform.position);
        this.enabled=false;
        animController.SetDead();
        Destroy(gameObject,3);
    }

    public void SetAnimations()
    {
    
        if (actorBusy) return;
        
        if (agent.velocity == Vector3.zero)
        { animController.SetMovingState(false); }
        else
        { animController.SetMovingState(true); }

    }
    private void OnMouseEnter()
    {
        BarCanvas.GetComponent<CanvasGroup>().DOFade(1, 1);
    }
    private void OnMouseOver()
    {
        BarCanvas.LookAt(Camera.main.transform);
    }
    private void OnMouseExit()
    {
        if(target==null)
        BarCanvas.GetComponent<CanvasGroup>().DOFade(0, 1);
    }

    private void Update()
    {
       
        FollowTarget();
        SetAnimations();
    
    }

}
