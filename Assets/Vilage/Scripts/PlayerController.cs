using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string ATTACK = "Attack";
    const string ATTACK_SWORD = "SwordSlash";
    const string ATTACK_GUN = "GunShot";


    CustomActions input;
    
    NavMeshAgent agent;
    Animator animator;
    public Interactable target;
    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] ParticleSystem clickEffectTarget;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] LayerMask InteractableLayers;
    Vector3 lookTarget;
    [Header("Attack")]
    
    float dis;
   [SerializeField] ParticleSystem hitEffect;
   public bool playerBusy = false;
    [SerializeField] float InteractDistance = 1.5f;
    float lookRotationSpeed = 8f;

  

   
  

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        input = new CustomActions();
        AssignInputs();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
        input.Main.Interact.performed += ctx => InteractWith();
    }
    void InteractWith()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, InteractableLayers) &&!playerBusy)
        {
            hit.transform.CompareTag("Interactable");
            target = hit.transform.GetComponent<Interactable>();
            if (clickEffect != null)
            { Instantiate(clickEffectTarget, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
        }
    }

    void ClickToMove()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers) ) 
        {
            target = null;
            playerBusy = false;
            agent.destination = hit.point;
            if(clickEffect != null)
            { Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
         
        }
    }
    void FollowTarget()
    {
        if (target == null) return;
        if (target.interactionType == InteractableType.Enemy)
        {
            dis =GetComponent<Ally>().weapon.attackDistance;
        }
          
        else
            dis = InteractDistance;
        if (Vector3.Distance(target.transform.position, transform.position) <= dis)
        { ReachDistance(); }
        else
        { agent.SetDestination(target.transform.position); }
    }
    void ReachDistance()
    {
        agent.SetDestination(transform.position); //durdur

        if (playerBusy) return;

        playerBusy = true;

        switch (target.interactionType)
        {
            case InteractableType.Enemy:

                switch (GetComponent<Ally>().weapon.weponType) //sliah tipine gore anim oynar
                {
                    case Weapon.WeaponType.None:
                        animator.Play(ATTACK);
                        break;
                    case Weapon.WeaponType.Sword:
                        animator.Play(ATTACK_SWORD);
                        break;
                    case Weapon.WeaponType.Gun:
                        animator.Play(ATTACK_GUN);
                        break;
                }
               

                  Invoke(nameof(SendAttack), GetComponent<Ally>().weapon. attackDelay);
         
                Invoke(nameof(ResetBusyState), GetComponent<Ally>().weapon.attackSpeed);
                break;
            case InteractableType.Item:

                target.InteractWithItem();
                target = null;

                Invoke(nameof(ResetBusyState), 0.2f);
                break;
            case InteractableType.Ally:
                target.InteractWithAlly();
                target = null;
                Invoke(nameof(ResetBusyState), 0.5f);
                break;
        }
    }
    void SendAttack()
    {
        if (target == null) return;

        if (target.myActor.currentHealth <= 0)
        { target = null; return; }

        
        Instantiate(hitEffect, target.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        target.GetComponent<Actor>().TakeDamage(GetComponent<Ally>().weapon.attackDamage);
        
        
    }
    

    void ResetBusyState()
    {
        playerBusy = false;
        SetAnimations();
    }

    void OnEnable() 
    { input.Enable(); }

    void OnDisable() 
    { input.Disable();}

    void Update() 
    {
        FollowTarget();
        FaceTarget();
        SetAnimations();
    }

    void FaceTarget()
	{
        if (target)
        {
           
            Vector3 dir = (target.transform.position- transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * lookRotationSpeed);
            
        }
        if (agent.destination == transform.position) return;
      
        
            Vector3 facing = Vector3.zero;
            if (target != null)
            { facing = target.transform.position; }
            else
            { facing = agent.destination; }

            Vector3 direction = (facing - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        
 
	}

    void SetAnimations()
    {
        if (playerBusy) return;
        if(agent.velocity == Vector3.zero)
        { animator.Play(IDLE); }
        else
        { animator.Play(WALK); }
    }

}
