using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon :ItemBase
{

    public GameObject SwordMesh;
   public float attackSpeed = 1.5f;
   public float attackDelay = 0.3f;
   public float attackDistance = 5f;
   public int attackDamage = 1;
    public WeaponType weponType;
    public enum WeaponType
    {
        None,
        Sword,
        Gun
    }
    public override void UseItem(Actor act)
    {
        act.weapon = this;
        if (act.weponTransform.childCount==0)
        {
            if (SwordMesh != null)
            {
                GameObject wpon = Instantiate(SwordMesh, act.weponTransform);
            }
      
          
        }
        else
        {
            
            Destroy(act.weponTransform.GetChild(0).gameObject);
            if (SwordMesh)
            {
                GameObject wpon = Instantiate(SwordMesh, act.weponTransform);
            }
            
        
        }
       

    }

  
}
