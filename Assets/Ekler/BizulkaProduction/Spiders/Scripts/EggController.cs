using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController :MonoBehaviour
{
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;
    public Enemy[] enemyAllys;
    public int currentHealt;
    public List<GameObject> prefabItems;//ilerde oldurdugunde spwnlancak objemiz
    public int itemChance=50;
    public void Clear()
    {
        _full.gameObject.SetActive(true);
        _damaged.gameObject.SetActive(false); 
    }
    public void DestroyEgg()
    {
        _full.gameObject.SetActive(false);
        _damaged.gameObject.SetActive(true);
        _particleSystem.Play();
    }

    public void TakeDamage(int amount)
    {

        currentHealt -= amount;
            
            foreach (var enemy in enemyAllys)
            {
                if (enemy)
                    enemy.AttackModeOn();

            }
        


        if (currentHealt <= 0)
        { Death(); }
    }
    public void Death()
    {
        foreach (var enemy in enemyAllys)
        {
            if (enemy)
            {
                GameManager.Instance.playerActor.GetComponent<PlayerController>().target = enemy.transform.GetComponent<Interactable>();
            }
        }
        DestroyEgg();
        Invoke("SpwnObject", 1);
        Destroy(gameObject, 3);
    }
  public void SpwnObject()
    {
        int rand = Random.Range(0,100);
        if (rand < itemChance)
        {
            int randItem = Random.Range(0, prefabItems.Count);
          GameObject obj = Instantiate(prefabItems[randItem], transform.position, prefabItems[randItem].transform.rotation);
            if (obj.GetComponent<Armor>())
            {
                if (obj.GetComponent<Armor>().armorType==Armor.ArmorType.Head)
                {
                    obj.transform.position -= new Vector3(0, 1.6f, 0);
                    Debug.Log("hi");
                    
                }
            }
        }
    }
}