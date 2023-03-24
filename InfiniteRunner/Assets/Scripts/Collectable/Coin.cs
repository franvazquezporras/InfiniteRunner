using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    int score;
    private GameManager gm;
    private AudioSource coinSound;
    private void Awake()
    {
        score = Random.Range(1, 5);
        gm = FindObjectOfType<GameManager>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER)
        {
            gameObject.SetActive(false);
            gm.SetScore(score);
            if (coinSound.isPlaying)
                coinSound.Stop();
            coinSound.Play();
            //Sumar puntos a jugador (controlar multiplicadores activos)
        }

    }
}
