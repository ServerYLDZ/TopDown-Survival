using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ActorClass
{
    Leader,
    Farmer,
    Enginer,
    Soldier
}
public enum ActorState
{   
    None,
    Follow,
    Farming,
    Chopping,
    Attack,

}
public class Actor : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
  

    public ActorInfoPanel InfoPanel;
    public Transform weponTransform;
    public int maxHealth;
    public Sprite ClassSprite;
    public ActorClass myClass;
    public ActorState currentState;
    public float FarmSpeed;
    public float WoodSpeed;
    public float FixSpeed;
    public float HealSpeed;
    public float Hunger;
    public Transform target;
    [SerializeField] public int currentHealth;
    public float lookRotationSpeed = 8f;
    public Weapon weapon;
    public ItemSlot weponSlot;

    public string ActName;
  [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;
    public bool actorBusy = false;
     public ParticleSystem hitEffect;
    void Awake()
    { 
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(SetAnimationSlowly());
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        Destroy(gameObject);
    }
   


    public void FollowPlayer()
    {

        if (myClass != ActorClass.Leader &&ActorState.Follow==currentState&&!target) //targeti daha sonradan degismeyi ve false yapmayi unutma
        {
            for (int i = 0; i < GameManager.Instance.isActorFolowPlayerPosUseForNow.Length; i++) //bos nokta bul ve oraya git
            {
                if (GameManager.Instance.isActorFolowPlayerPosUseForNow[i] == false)
                {
                    target = GameManager.Instance.actorFolowPlayerPos[i];
                    GameManager.Instance.isActorFolowPlayerPosUseForNow[i] = true;
                    break;
                }
            }
           
        }
        
        agent.SetDestination(target.position);

        RotateTarget();

    }
    public void RotateTarget()
    {
        if (target)
        {
            if (agent.destination == transform.position) return;
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * lookRotationSpeed);
           
        }
    }
   public void SetAnimations()
    {
        if (ActorState.None == currentState ||myClass==ActorClass.Leader) return;
        if (actorBusy) return;
        if (agent.velocity == Vector3.zero)
        { animator.Play(IDLE); }
        else
        { animator.Play(WALK); }

    }
    public IEnumerator SetAnimationSlowly()
    {
        yield return new WaitForSeconds(.1f);
        SetAnimations();
        StartCoroutine(SetAnimationSlowly());
    }
}
