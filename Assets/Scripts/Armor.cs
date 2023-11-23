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
    public float Healt;
    public int armor;
    public float speed;

    public override void UseItem(Ally act)
    {
   


    }

}
