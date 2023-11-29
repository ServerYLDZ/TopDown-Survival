using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActorInfoPanel : MonoBehaviour
{
    public Ally targetActor;

    public TMP_Text Health;
    public TMP_Text myClass;
    public TMP_Text FarmSpeed;
    public TMP_Text WoodSpeed;
    public TMP_Text Armor;
    public TMP_Text Speed;
    public TMP_Text attackSpeed;
    public TMP_Text attackDistance;
    public TMP_Text attackDamage;
    public TMP_Text starve;
    public TMP_Text LevelText;

    public bool isOpen=false;
    private void OnEnable()
    {
        SetInfoPanel();
    }
    private void Start()
    {
        SetInfoPanel();
    }
    public void SetInfoPanel()
    {
       
        Health.text = targetActor.currentHealth.ToString()+"/"+targetActor.maxHealth.ToString();
        myClass.text = targetActor.myClass.ToString();
        FarmSpeed.text = targetActor.FarmSpeed.ToString();
        WoodSpeed.text = targetActor.WoodSpeed.ToString();
        Armor.text = targetActor.armor.ToString();
        Speed.text = targetActor.agent.speed.ToString();
        attackSpeed.text = targetActor.weapon.attackSpeed.ToString();
        attackDistance.text = targetActor.weapon.attackDistance.ToString();
        attackDamage.text = targetActor.weapon.attackDamage.ToString();
        starve.text = targetActor.Hunger.ToString();
        LevelText.text = targetActor.Level.ToString();
    }
    public void OpenCloseInfoPanel()
    {
        isOpen = !isOpen;
        gameObject.SetActive(isOpen);
        if (isOpen)
        {
            GameManager.Instance.CurrentActor = targetActor;
            
        }
        else
        {
            GameManager.Instance.CurrentActor = GameManager.Instance.playerActor;
        }
    }
}
