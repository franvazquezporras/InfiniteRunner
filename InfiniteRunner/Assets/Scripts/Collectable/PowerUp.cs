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

    private void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layers.PLAYER)
        {
            powerUpManager.ActivatePowerUp(doublePoints, shield, easyMode, powerUpDuration);
        }
        gameObject.SetActive(false);
    }
}
