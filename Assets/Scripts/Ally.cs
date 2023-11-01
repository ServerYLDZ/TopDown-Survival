using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Actor
{
    const string IDLE = "Idle";
    const string WALK = "Walk";


    public ActorInfoPanel InfoPanel;
    public Transform weponTransform;
    public Sprite ClassSprite;
    public float FarmSpeed;
    public float WoodSpeed;
    public float FixSpeed;
    public float HealSpeed;
    public float Hunger;
    public string ActName;

    public ParticleSystem hitEffect;

 
    public Weapon weapon;
    public ItemSlot weponSlot;
   

    void Start()
    {
        StartCoroutine(SetAnimationSlowly());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FollowPlayer()
    {

        if (myClass != ActorClass.Leader && ActorState.Follow == currentState && !target && myClass != ActorClass.Enemy) //targeti daha sonradan degismeyi ve false yapmayi unutma
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

    public void SetAnimations()
    {
        if (ActorState.None == currentState || myClass == ActorClass.Leader || myClass == ActorClass.Enemy) return;
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
