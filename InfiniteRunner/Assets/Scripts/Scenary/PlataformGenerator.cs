using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformGenerator : MonoBehaviour
{
    
    [SerializeField] private Transform generationPoint;
    [SerializeField] private float distance;
    private float minDistance = 1;
    private float maxDistance = 5;
    private int platformSelectetor;
    private float[] platformsWidth;
    [SerializeField] private ItemPool[] poolPlatforms;
    
    private float minHeight;
    private float maxHeight;
    [SerializeField] private Transform maxHeightPoint;
    [SerializeField] private float maxHeightChange;
    private float heightChange;

    void Start()
    {     
        platformsWidth = new float[poolPlatforms.Length];
        for (int i = 0; i < poolPlatforms.Length; i++)
            platformsWidth[i] = poolPlatforms[i].poolItem.GetComponent<BoxCollider2D>().size.x;

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
    }

    
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distance = Random.Range(minDistance, maxDistance);
            platformSelectetor = Random.Range(0, poolPlatforms.Length);
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if(heightChange > maxHeight)            
                heightChange = maxHeight;
            else if(heightChange < minHeight)
                heightChange = minHeight;

            transform.position = new Vector3(transform.position.x + (platformsWidth[platformSelectetor]/2) + distance, heightChange, transform.position.z);
            
            Instantiate(poolPlatforms[platformSelectetor], transform.position, transform.rotation);
            GameObject newPlatform = poolPlatforms[platformSelectetor].GetPoolItem();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
            transform.position = new Vector3(transform.position.x + (platformsWidth[platformSelectetor] / 2), transform.position.y, transform.position.z);
        }
    }
}
