using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    int score;

    private void Awake()
    {
        score = Random.Range(1, 5);
    }

    private void OnDestroy()
    {
        //Sumar puntos a jugador (controlar multiplicadores activos)
    }
}
