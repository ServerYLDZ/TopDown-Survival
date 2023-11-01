using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjet/NewItem", order = 1)]
public class Item : ScriptableObject
{
    
   public ItemTpe type;
    public ItemBase prefab;
    public bool stackable = false;
    public bool usable = false;
    public int count;
    public Sprite image;
    public string description;
    public string Name;
    
}
