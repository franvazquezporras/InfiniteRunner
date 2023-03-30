using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private bool doublePoints;
    [SerializeField] private bool shield;
    [SerializeField] private bool easyMode;
    [SerializeField] private float powerUpDuration;    
    private PowerUpManager powerUpManager;
    private AudioSource powerUpSound;

    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: la referencia de sonido y el manager de powerups                                                                  */
    /*********************************************************************************************************************************/
    private void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        powerUpSound = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
    }

    /*********************************************************************************************************************************/
    /*Funcion: OnTriggerEnter2D                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: colision del player                                                                                     */
    /*Descripción: Comprueba cuando se a colisionado con el player para activar el powerUp                                           */
    /*********************************************************************************************************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layers.PLAYER)
        {
            
            powerUpManager.ActivatePowerUp(doublePoints, shield, easyMode, powerUpDuration);
            powerUpSound.Play();

        }
        gameObject.SetActive(false);
    }
}
