using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Item item;

    public virtual void UseItem()
    {
        Debug.Log("hi");
    }
    public virtual void UseItem(Ally act)
    {
        Debug.Log("hi");
    }
}
