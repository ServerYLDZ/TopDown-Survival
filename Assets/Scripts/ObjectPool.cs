using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectPool : MonoSingleton<ObjectPool>
{
    private GameObject tmp;
    private int tmpIndex;
    [Serializable]
    public struct Pool
    {
        public List<GameObject> pooledObject;
        public GameObject pooledPrefab;
        public int poolSize;
    }
    [SerializeField] public Pool[] pools;
    private void Awake()
    {
        // pool oluşturuldu
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObject = new List<GameObject>();

            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].pooledPrefab, this.gameObject.transform);
                obj.SetActive(false);
                pools[j].pooledObject.Add(obj);

            }
        }
    }
    public GameObject GetPooledObject(int poolIndex)
    {

        if (poolIndex >= pools.Length) return null;
        
        for (int k = 0; k < pools[poolIndex].pooledObject.Count; k++)
        { //kullanılmayan bir objeyi getirsin diye  ekstra bir kod ekledim
            if (!pools[poolIndex].pooledObject[k].activeInHierarchy)
            {
                tmp = pools[poolIndex].pooledObject[k];
                tmpIndex = k;
                
                break;
            }

        }

        GameObject obj = tmp;
        pools[poolIndex].pooledObject.RemoveAt(tmpIndex);
        //GameObject obj = pools[poolIndex].pooledObject.Dequeue(); //ilk kuyruk objeyi çıkar kullan


        obj.SetActive(true);
        
        // pools[poolIndex].pooledObject.Enqueue(obj); //sonra geri kuryuga sonuna ekle
        pools[poolIndex].pooledObject.Add(obj);
        return obj;

    }

}
