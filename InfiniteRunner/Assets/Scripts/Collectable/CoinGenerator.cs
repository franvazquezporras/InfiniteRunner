using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private ItemPool coinPool;
    [SerializeField] private float distance;


    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPool.GetPoolItem();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);
        GameObject coin2 = coinPool.GetPoolItem();
        coin2.transform.position = new Vector3(startPosition.x-distance,startPosition.y,startPosition.z);
        coin2.SetActive(true);
        GameObject coin3 = coinPool.GetPoolItem();
        coin3.transform.position = new Vector3(startPosition.x + distance, startPosition.y, startPosition.z);
        coin3.SetActive(true);
    }
}
