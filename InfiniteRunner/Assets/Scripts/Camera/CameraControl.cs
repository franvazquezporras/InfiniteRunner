using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    //Variables
    public Transform player;
    public float smothing = 5f;

    private Vector3 offset;


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: obtiene la posicion inicial de la camara en el player                                                             */
    /*********************************************************************************************************************************/
    private void Start()
    {
        offset = transform.position - player.position;
    }

    /*********************************************************************************************************************************/
    /*Funcion: FixedUpdate                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Actualiza la posicion de la camara suavizando su movimiento tras el player                                        */
    /*********************************************************************************************************************************/
    private void FixedUpdate()
    {
        Vector3 targetCamPos = new Vector3(player.position.x + offset.x+10,0,-10); 
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smothing * Time.deltaTime); 

    }
}
