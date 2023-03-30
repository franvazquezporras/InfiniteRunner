using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    //Variables
    [SerializeField] private ItemPool coinPool;
    [SerializeField] private float distance;

    /*********************************************************************************************************************************/
    /*Funcion: SpawnCoins                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: posicion inicial de la moneda central                                                                   */                                   
    /*Descripción: Genera 3 monedas en el centro de la plataforma                                                                    */
    /*********************************************************************************************************************************/
    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPool.GetPoolItem();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);
        GameObject coin2 = coinPool.GetPoolItem();
        coin2.transform.position = new Vector3(startPosition.x-distance,startPosition.y,startPosition.z);
        coin2.SetActive(true);
        GameObject coin3 = coinPool.GetPoolItem();
        coin3.transform.position = new Vector3(startPosition.x + distance, startPosition.y, startPosition.z);
        coin3.SetActive(true);
    }
}
