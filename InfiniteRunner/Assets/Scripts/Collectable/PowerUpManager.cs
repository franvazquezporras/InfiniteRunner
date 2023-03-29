using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool doublePoints;
    private bool shield;
    private bool easyMode;
    private bool powerUpActive;
    private bool firstLoad = true;
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

    private AudioSource powerUpSound;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        plataformGenerator = FindObjectOfType<PlataformGenerator>();
        powerUpSound = GameObject.Find("EndPowerUpSound").GetComponent<AudioSource>();
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
                plataformGenerator.SetMinDistance(1.5f);
                plataformGenerator.SetMaxDistance(2f);
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
                powerUpSound.Play();
            }
        }
    }
    public void ActivatePowerUp(bool _doublePoints, bool _shield, bool _easyMode, float _powerUpDuration)
    {
        doublePoints = _doublePoints;
        shield = _shield;
        powerUpDuration = _powerUpDuration;
        easyMode = _easyMode;
        gm.SetScore(100);
        if (firstLoad)
        {        
            normalPointPerSecond = gm.GetPointForSecond();
            spikeRate = plataformGenerator.GetRandomSpike();
            normalMinDistance = plataformGenerator.GetMinDistance();
            normalMaxDistance = plataformGenerator.GetMaxDistance();
            normalMaxHeightChange = plataformGenerator.GetMaxHeightChange();
            firstLoad = false;
        }
        powerUpActive = true;
        
    }
}
