using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    //Variables
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


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: asigna los valores basicos de las variables y obtiene las referencias                                             */
    /*********************************************************************************************************************************/
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        plataformGenerator = FindObjectOfType<PlataformGenerator>();
        powerUpSound = GameObject.Find("EndPowerUpSound").GetComponent<AudioSource>();
    }


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Control de powerups activos y tiempo de duracion                                                                  */
    /*********************************************************************************************************************************/
    private void Update()
    {
        if (powerUpActive)
        {
            powerUpDuration -= Time.deltaTime;
            IsDoublePoint();
            IsShield();
            IsEasyMode();
            CheckDurationPowerUp();
        }
    }


    /*********************************************************************************************************************************/
    /*Funcion: CheckDurationPowerUp                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla duracion del power up, cuando llegue a 0 se resetean las variables con su valor por defecto              */
    /*********************************************************************************************************************************/
    private void CheckDurationPowerUp()
    {
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



    /*********************************************************************************************************************************/
    /*Funcion: IsDoublePoint                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Comprueba si el powerup activo es el de doble puntuacion                                                          */
    /*********************************************************************************************************************************/
    private void IsDoublePoint()
    {
        if (doublePoints)
            gm.SetPointForSecond(normalPointPerSecond * 2);
    }

    /*********************************************************************************************************************************/
    /*Funcion: IsShield                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Comprueba si el powerup activo es el de sin pinchos                                                               */
    /*********************************************************************************************************************************/
    private void IsShield()
    {
        if (shield)
        {
            spikeList = FindObjectsOfType<PlataformDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
                if (spikeList[i].gameObject.layer == Layers.TRAP)
                    spikeList[i].gameObject.SetActive(false);
        }
    }

    /*********************************************************************************************************************************/
    /*Funcion: IsEasyMode                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Comprueba si el powerup activo es el de modo facil                                                                */
    /*********************************************************************************************************************************/
    private void IsEasyMode()
    {
        if (easyMode)
        {
            plataformGenerator.SetMinDistance(1.5f);
            plataformGenerator.SetMaxDistance(2f);
            plataformGenerator.SetRandomSpike(0);
            plataformGenerator.SetMaxHeightChange(0);

        }
    }


    /*********************************************************************************************************************************/
    /*Funcion: ActivatePowerUp                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: _doublePoints,_shield,_easyMode (tipo de poder que da el powerup) _powerUpDuration (duracion)           */
    /*Descripción: Obtiene las variables que se van activar y guarda los valores predeterminados de las variables que se van         */
    /*              a modificar                                                                                                      */
    /*********************************************************************************************************************************/
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
