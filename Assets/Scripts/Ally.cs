using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ally : Actor
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string DEAD = "Die";
    public int Level = 1;
    public float xp = 0;
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
                if (i <armor)
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
    public void TakeXP(float amout)
    {
        if (GameManager.Instance.xpLevelUpLimits[Level-1]<=xp+amout) //level UP mu
        {
            xp =   xp + amout- GameManager.Instance.xpLevelUpLimits[Level-1];
            Level++;
            maxHealth++;
            currentHealth++;        
            if (Level % 5 == 0)
            {
                armor++;
            }
            HUD.Instance.LevelSet();
            HUD.Instance.ArmorBarSet();

          if(GameManager.Instance.xpLevelUpLimits[Level - 1] <= xp)
            {
                TakeXP(0);
            }
        }
        else
        {
            xp += amout;
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
                    GetFloatingText("-" + (maxHealth-amount));
                    text.GetComponent<TextMesh>().color = Color.green;
                }
                GetFloatingText("-" + (amount));
                text.GetComponent<TextMesh>().color = Color.green;
            }
            else
            {
                if (amount > armor)
                {
                    currentHealth -= amount - armor;
                    HealthBar.DOScaleX((float)currentHealth / (float)maxHealth, .5f);
                    GetFloatingText("-" + (amount - armor));
                    text.GetComponent<TextMesh>().color = Color.red;
                }
                else
                {
                    currentHealth -= 0;
                    GetFloatingText("-" + (0));
                    text.GetComponent<TextMesh>().color = Color.yellow;
                }
            }
            HUD.Instance.HealtSet();
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
        if (myClass == ActorClass.Leader)
        {
            agent.enabled = false;
            target = null;
            animator.Play(DEAD);
            GameManager.Instance.isGameOver = true;         
            GetComponent<Collider>().enabled = false;
            GetComponent<PlayerController>().playerBusy = true;
            GetComponent<PlayerController>().target = null;
            GetComponent<PlayerController>().enabled = false;
        
            transform.GetChild(0).localPosition = new Vector3(0, -.7f, 0);
      
        }
        else
        {          
            actorBusy = true;
            target = null;
            animator.Play(DEAD);
            GetComponent<Collider>().enabled = false;
            GetComponent<ActorStateMacineController>().enabled = false;
            enabled = false;
            agent.enabled = false;
            transform.GetChild(0).localPosition = new Vector3(0, -.7f, 0);
      
        }
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
    public override void GetFloatingText(string damage)
    {
        base.GetFloatingText(damage);
    }
    public IEnumerator SetAnimationSlowly()
    {
        yield return new WaitForSeconds(.1f);
        SetAnimations();
        StartCoroutine(SetAnimationSlowly());
    }


}
