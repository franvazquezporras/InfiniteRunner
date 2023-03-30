using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformDestroyer : MonoBehaviour
{
    //Variables
    private GameObject platformDestroyerPoint;

    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene la referencia del punto de destruccion de las plataformas                                                 */
    /*********************************************************************************************************************************/
    void Start()
    {
        platformDestroyerPoint = GameObject.Find("PlatformDestoyer");
    }


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Comprueba cuando la posicion de la plataforma en x es menor que el punto de destruccion y desactiva la misma      */
    /*********************************************************************************************************************************/
    void Update()
    {
        if(transform.position.x < platformDestroyerPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }
}
