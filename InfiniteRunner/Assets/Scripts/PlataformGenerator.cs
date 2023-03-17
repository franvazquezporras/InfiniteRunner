using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Transform generationPoint;
    [SerializeField] private float distance;

    private float minDistance = 1;
    private float maxDistance = 5;

    private float platformWidth;
    void Start()
    {
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distance = Random.Range(minDistance, maxDistance);
            transform.position = new Vector3(transform.position.x + platformWidth + distance, transform.position.y, transform.position.z);
            Instantiate(platform, transform.position, transform.rotation);

        }
    }
}
