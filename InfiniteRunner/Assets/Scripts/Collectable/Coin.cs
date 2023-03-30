using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    //Variables
    int score;
    private GameManager gm;
    private AudioSource coinSound;

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: asigna los valores basicos de las variables y obtiene las referencias                                             */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        score = Random.Range(1, 5);
        gm = FindObjectOfType<GameManager>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    /*********************************************************************************************************************************/
    /*Funcion: OnTriggerEnter2D                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: colision del player                                                                                     */
    /*Descripción: Comprueba cuando el player a colisionado con la moneda, esta suma los puntos y desactiva la moneda                */
    /*              ejecutando el sonido de la misma                                                                                 */
    /*********************************************************************************************************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER)
        {
            gameObject.SetActive(false);
            gm.SetScore(score);
            if (coinSound.isPlaying)
                coinSound.Stop();
            coinSound.Play();            
        }

    }
}
