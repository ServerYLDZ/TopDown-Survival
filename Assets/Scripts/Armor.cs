using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : ItemBase
{
    public enum ArmorType
    {
        Shield,
        Head,
        Chest,
        Foot
    }
    public ArmorType armorType;
    public GameObject armorMesh;
    public int Health;
    public int armor;
    public float speed;

    public override void UseItem(Ally act)
    {

        switch (armorType)
        {
            case ArmorType.Shield:
                
                    if (armorMesh != null)
                    {
                        GameObject shield = Instantiate(armorMesh, act.shieldTransform);
                        act.armor += armor;
                        act.maxHealth += Health;
                        act.currentHealth += Health;
                        act.agent.speed += speed;
                    }


                
         
                break;
            case ArmorType.Head:
                if (armorMesh != null)
                {
                    GameObject shield = Instantiate(armorMesh, act.HeadTransform);
                    act.armor += armor;
                    act.maxHealth += Health;
                    act.currentHealth += Health;
                    act.agent.speed += speed;
                }
                break;
            case ArmorType.Chest:
                act.armor += armor;
                act.maxHealth += Health;
                act.currentHealth += Health;
                act.agent.speed += speed;
                break;
            case ArmorType.Foot:
                act.armor += armor;
                act.maxHealth += Health;
                act.currentHealth += Health;
                act.agent.speed += speed;
                break;
            default:
                break;
        }
        HUD.Instance.ArmorBarSet();
    }

    public void UnUseArmor(Ally act)
    {
     
            act.armor -= armor;
            act.maxHealth -= Health;
            act.agent.speed -= speed;
            if (act.currentHealth > act.maxHealth)
             {
            act.currentHealth = act.maxHealth;
             }
        if (armorType == ArmorType.Shield && act.shieldTransform.childCount>0)
        {
            Debug.Log(act.shieldTransform.GetChild(0).gameObject);
            Destroy(act.shieldTransform.GetChild(0).gameObject);
        }
        if (armorType == ArmorType.Head && act.HeadTransform.childCount > 0)
        {
            Debug.Log(act.HeadTransform.GetChild(0).gameObject);
            Destroy(act.HeadTransform.GetChild(0).gameObject);
        }

        HUD.Instance.ArmorBarSet();
    }
}
