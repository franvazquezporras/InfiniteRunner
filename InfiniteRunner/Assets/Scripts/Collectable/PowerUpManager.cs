using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool doublePoints;
    private bool shield;
    private bool easyMode;
    private bool powerUpActive;
    private float powerUpDuration;
    private GameManager gm;
    private PlataformGenerator plataformGenerator;
    
    //normalize params
    private float normalPointPerSecond;
    private float spikeRate;
    private float normalMinDistance;
    private float normalMaxDistance;
    private float normalMaxHeightChange;

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
                spikeList = FindObjectsOfType<PlataformDestroyer>();
                for (int i = 0; i < spikeList.Length; i++)                
                    if(spikeList[i].gameObject.layer == Layers.TRAP)
                        spikeList[i].gameObject.SetActive(false);                
            }
            if (easyMode)
            {                
                plataformGenerator.SetMinDistance(1f);
                plataformGenerator.SetMaxDistance(1.5f);
                plataformGenerator.SetRandomSpike(0);
                plataformGenerator.SetMaxHeightChange(0);
            }
            if (powerUpDuration <= 0)
            {
                gm.SetPointForSecond(normalPointPerSecond);
                plataformGenerator.SetRandomSpike(spikeRate);
                plataformGenerator.SetMinDistance(normalMinDistance);
                plataformGenerator.SetMaxDistance(normalMaxDistance);
                plataformGenerator.SetMaxHeightChange(normalMaxHeightChange);
                powerUpActive = false;
            }
                
        }
    }
    public void ActivatePowerUp(bool _doublePoints,bool _shield,bool _easyMode,float _powerUpDuration)
    {
        doublePoints = _doublePoints;
        shield = _shield;
        powerUpDuration = _powerUpDuration;
        easyMode = _easyMode;

        normalPointPerSecond = gm.GetPointForSecond();
        spikeRate = plataformGenerator.GetRandomSpike();
        normalMinDistance = plataformGenerator.GetMinDistance();
        normalMaxDistance = plataformGenerator.GetMaxDistance();
        normalMaxHeightChange = plataformGenerator.GetMaxHeightChange();
        powerUpActive = true;
        
    }
}
