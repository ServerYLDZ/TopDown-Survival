using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ActorClass
{
    Leader,
    Farmer,
    Enginer,
    Soldier,
    Enemy
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
   
    public int maxHealth;
   
    public ActorClass myClass;
    public int armor; 
    public int currentHealth;
  

   
  [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    public ActorState currentState;
    public bool actorBusy = false;
    public Transform target;
    public float lookRotationSpeed = 8f;

    void Awake()
    { 
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        { Death(); }
    }

    public virtual void  Death()
    {
        if (myClass == ActorClass.Leader)
        {
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.playerActor.GetComponent<PlayerController>().enabled = false;
            GameManager.Instance.playerActor.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
     
    }
   


    public virtual void FollowPlayer()
    {

        if (myClass != ActorClass.Leader &&ActorState.Follow==currentState&&!target && myClass != ActorClass.Enemy) //targeti daha sonradan degismeyi ve false yapmayi unutma
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
 

}
