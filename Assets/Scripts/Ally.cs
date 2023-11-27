using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ally : Actor
{
    const string IDLE = "Idle";
    const string WALK = "Walk";


    public ActorControlItem actorControlItem;
    public ActorInfoPanel InfoPanel;
    public Transform weponTransform;
    public Transform shieldTransform;
    public Transform HeadTransform;
    public Sprite ClassSprite;
    public float FarmSpeed;
    public float WoodSpeed;
    public float Hunger;
  [HideInInspector]  public float MaxHunger = 100;
    public string ActName;
   [HideInInspector] public int AllyIndex;
    public ParticleSystem hitEffect;

 
    public Weapon weapon;
    public WeaponSlot weponSlot;
    public HeadSlot headSlot;
    public ChestSlot chestSlot;
    public ShieldSlot slieldSlot;
    public FootSlot footSlot;
    public CanvasGroup Bar;
    public RectTransform HealthBar;
    public Transform ArmorBar;


    void Start()
    {
        StartCoroutine(SetAnimationSlowly());
        InvokeRepeating(nameof(DecreseHunger), 0, 100);
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

        if (currentHealth > 0 )//karakterin cani max healti gecemez
        {
            if (amount < 0)
            {
                //can dolduruyordur burdan devam ke
                currentHealth -= amount;
                if (currentHealth >= maxHealth)
                {
                    HealthBar.DOScaleX(1, .5f);
                    currentHealth = maxHealth;
                }
              
            }
            else
            {
                if (amount > armor)
                {
                    currentHealth -= amount - armor;
                    HealthBar.DOScaleX((float)currentHealth / (float)maxHealth, .5f);
               
                }
                else
                {
                    currentHealth -= 0;
                }
            }
        }
      

        if (currentHealth <= 0)
        {
            HealthBar.DOScaleX(0, .5f);
            currentHealth =0;
            Death(); 
        }
    }
    public override void Death()
    {
        Bar.DOFade(0, 1f);
        base.Death();
    }

    public override void FollowPlayer()
    {

        if (myClass != ActorClass.Leader && ActorState.Follow == currentState && !target && myClass != ActorClass.Enemy) //targeti daha sonradan degismeyi ve false yapmayi unutma
        {
          
            for (int i = 0; i < GameManager.Instance.isActorFolowPlayerPosUseForNow.Length; i++) //bos nokta bul ve oraya git
            {
                
                if (GameManager.Instance.isActorFolowPlayerPosUseForNow[i] == false)
                {
                    
                    target = GameManager.Instance.actorFolowPlayerPos[i];
                    AllyIndex = i;
                    GameManager.Instance.isActorFolowPlayerPosUseForNow[i] = true;
                    break;
                }
            }

        }
        if (target)
        {
            agent.SetDestination(target.position);

            RotateTarget();
        }
     

    }

    public void SetAnimations()
    {
        if (ActorState.None == currentState || myClass == ActorClass.Leader || myClass == ActorClass.Enemy) return;
        if (actorBusy) return;
        if (agent.velocity == Vector3.zero)
        { animator.Play(IDLE); }
        else
        { animator.Play(WALK); }

    }
    public void DecreseHunger()
    {
        
        if (Hunger <= 0)
        {

            Hunger = 0;
            currentHealth--;
            if (currentHealth <= 0)
            {
                Death();
            }
           
        }
        else
        {
            Hunger--;
        }

    }
    public IEnumerator SetAnimationSlowly()
    {
        yield return new WaitForSeconds(.1f);
        SetAnimations();
        StartCoroutine(SetAnimationSlowly());
    }


}
