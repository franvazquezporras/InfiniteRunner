using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    int salto = 350;
    [SerializeField]
    float velocidad = 5;

    // Update is called once per frame
    void Update()
    {
        //movimiento
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidad, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        
    }
}
