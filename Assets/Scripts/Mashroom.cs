using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MashroomType
{
    None,
    HealtyMashroom,
    UnHealtyMashroom,
    BufferMushrom
}
public class Mashroom : ItemBase
{

    public int foodAmount=1;
    public int healEffectAmount =1;
    public MashroomType type;
    public override void UseItem(Ally act)
    {
        switch (type)
        {
            case MashroomType.HealtyMashroom:
                act.Hunger += foodAmount;
                act.TakeDamage(-healEffectAmount);
                break;
            case MashroomType.UnHealtyMashroom:
                act.Hunger += foodAmount;
                act.TakeDamage(healEffectAmount);
                break;
            case MashroomType.BufferMushrom:
                break;
        }
    }
}
