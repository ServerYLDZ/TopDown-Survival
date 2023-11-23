using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public List<Ally> inRangeAlly;
    public float foodFeedCount=1;
    private void Start()
    {
        InvokeRepeating("FeedAlly", 0, 60);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ally>())
        {
            inRangeAlly.Add(other.GetComponent<Ally>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ally>())
        {
            inRangeAlly.Remove(other.GetComponent<Ally>());
        
        }
        
    }

    public void FeedAlly()
    {
        if (inRangeAlly.Count>0)
        {
            for (int i = 0; i < inRangeAlly.Count; i++)
            {
                if (inRangeAlly[i].Hunger < inRangeAlly[i].MaxHunger && GameManager.Instance.Food >= 1)
                {
                    inRangeAlly[i].Hunger += foodFeedCount;
                    GameManager.Instance.Food -= foodFeedCount;
                }
            
            }
        }
    }
}
