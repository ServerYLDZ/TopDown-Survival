using UnityEngine;

public class EggController :MonoBehaviour
{
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;
    public Enemy[] enemyAllys;
    public int currentHealt;
    public GameObject prefabItem;//ilerde oldurdugunde spwnlancak objemiz
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
        Destroy(gameObject, 3);
    }
}