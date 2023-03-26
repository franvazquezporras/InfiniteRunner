using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformGenerator : MonoBehaviour
{
    
    [SerializeField] private Transform generationPoint;
    [SerializeField] private float distance;
    private float minDistance = 1;
    private float maxDistance = 5;
    private int platformSelected;
    private float[] platformsWidth;
    [SerializeField] private ItemPool[] poolPlatforms;
    
    private float minHeight;
    private float maxHeight;
    [SerializeField] private Transform maxHeightPoint;
    [SerializeField] private float maxHeightChange;
    private float heightChange;

    private CoinGenerator coinGenerator;
    [SerializeField] private float randomCoin;

    [SerializeField] private float randomSpike;
    [SerializeField] ItemPool SpikePool;

    [SerializeField] private float powerUpHeight;
    [SerializeField] private ItemPool [] powerUpPool;
    [SerializeField] private float randomPowerUp;

    public float GetRandomSpike() { return randomSpike; }
    public void SetRandomSpike(float _randomSpike) { randomSpike = _randomSpike; }
    void Start()
    {     
        platformsWidth = new float[poolPlatforms.Length];
        for (int i = 0; i < poolPlatforms.Length; i++)
            platformsWidth[i] = poolPlatforms[i].poolItem.GetComponent<BoxCollider2D>().size.x;

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        coinGenerator = FindObjectOfType<CoinGenerator>();
    }

    
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distance = Random.Range(minDistance, maxDistance);
            platformSelected = Random.Range(0, poolPlatforms.Length);
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if(heightChange > maxHeight)            
                heightChange = maxHeight;
            else if(heightChange < minHeight)
                heightChange = minHeight;

            if(Random.Range(0f,100)< randomPowerUp)
            {
                GameObject newPowerUp = powerUpPool[Random.Range(0,powerUpPool.Length)].GetPoolItem();
                newPowerUp.transform.position = transform.position + new Vector3(distance / 2f, powerUpHeight, 0f);
                newPowerUp.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformsWidth[platformSelected]/2) + distance, heightChange, transform.position.z);
            
            Instantiate(poolPlatforms[platformSelected], transform.position, transform.rotation);
            GameObject newPlatform = poolPlatforms[platformSelected].GetPoolItem();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if(Random.Range(0f,100f)< randomCoin)
                coinGenerator.SpawnCoins(new Vector3(transform.position.x,transform.position.y+3f,transform.position.z));

            if (Random.Range(0f, 100f) < randomSpike)
            {
                GameObject spike = SpikePool.GetPoolItem();
                
                Vector3 spikePos = new Vector3(Random.Range(-platformsWidth[platformSelected]/2+1f, platformsWidth[platformSelected] / 2-1f), 2f, 0f);
                spike.transform.position = transform.position+spikePos;
                spike.transform.rotation = transform.rotation;
                spike.SetActive(true);
            }
                

                transform.position = new Vector3(transform.position.x + (platformsWidth[platformSelected] / 2), transform.position.y, transform.position.z);
        }
    }
}
