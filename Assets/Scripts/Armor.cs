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
                break;
            case ArmorType.Chest:
                break;
            case ArmorType.Foot:
                break;
            default:
                break;
        }

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
            
        
    
    }
}
