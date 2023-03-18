using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ItemPool : MonoBehaviour
{
    [SerializeField] private GameObject poolItem;
    [SerializeField] private int poolAmount;

    List<GameObject> poolObjects;
    // Start is called before the first frame update
    void Start()
    {
        poolObjects = new List<GameObject>();
        for(int i = 0; i< poolAmount; i++)
        {
            GameObject obj = Instantiate(poolItem);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject GetPoolItem()
    {
        for(int i = 0;i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
                return poolObjects[i];
        }
        GameObject obj = Instantiate(poolItem);
        obj.SetActive(false);
        poolObjects.Add(obj);
        return obj;
    }
}
