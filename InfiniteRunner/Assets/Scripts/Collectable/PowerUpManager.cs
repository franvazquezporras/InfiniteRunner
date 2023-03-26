using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool doublePoints;
    private bool shield;
    private bool powerUpActive;
    private float powerUpDuration;
    private GameManager gm;
    private PlataformGenerator plataformGenerator;
    private float normalPointPerSecond;
    private float spikeRate;

    private PlataformDestroyer[] spikeList;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        plataformGenerator = FindObjectOfType<PlataformGenerator>();
    }
    private void Update()
    {
        if (powerUpActive)
        {
            powerUpDuration -= Time.deltaTime;

            if (doublePoints)
                gm.SetPointForSecond(normalPointPerSecond * 2);
            if (shield)
            {
                //plataformGenerator.SetRandomSpike(0);
                spikeList = FindObjectsOfType<PlataformDestroyer>();
                for (int i = 0; i < spikeList.Length; i++)                
                    if(spikeList[i].gameObject.layer == Layers.TRAP)
                        spikeList[i].gameObject.SetActive(false);
                
            }
                

            if (powerUpDuration <= 0)
            {
                gm.SetPointForSecond(normalPointPerSecond);
                plataformGenerator.SetRandomSpike(spikeRate);
                powerUpActive = false;
            }
                
        }
    }
    public void ActivatePowerUp(bool _doublePoints,bool _shield,float _powerUpDuration)
    {
        doublePoints = _doublePoints;
        shield = _shield;
        powerUpDuration = _powerUpDuration;

        normalPointPerSecond = gm.GetPointForSecond();
        spikeRate = plataformGenerator.GetRandomSpike();
        powerUpActive = true;
        
    }
}
