using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformDestroyer : MonoBehaviour
{
    private GameObject platformDestroyerPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        platformDestroyerPoint = GameObject.Find("PlatformDestoyer");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < platformDestroyerPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }
}
