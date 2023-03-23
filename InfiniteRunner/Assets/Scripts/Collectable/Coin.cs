using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    int score;
    private GameManager gm;
    private void Awake()
    {
        score = Random.Range(1, 5);
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER)
        {
            gameObject.SetActive(false);
            gm.SetScore(score);
            //Sumar puntos a jugador (controlar multiplicadores activos)
        }

    }
}
