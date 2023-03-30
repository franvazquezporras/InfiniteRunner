using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{    
    //Variables
    [SerializeField] private GameObject background1, background2, background3, background4;
    [SerializeField] private float scrollSpeed;

    private Renderer bg1R, bg2R, bg3R, bg4R;
    private float iniCamX, difCamX;

    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene referencias de las distintas capas de background                                                          */
    /*********************************************************************************************************************************/
    private void Start()
    {
        bg1R = background1.GetComponent<Renderer>();
        bg2R = background2.GetComponent<Renderer>();
        bg3R = background3.GetComponent<Renderer>();
        bg4R = background4.GetComponent<Renderer>();
        iniCamX = transform.position.x;
    }


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza la posicion de los backgrounds                                                                          */
    /*********************************************************************************************************************************/
    private void Update()
    {
        UpdateBackground();
    }

    /*********************************************************************************************************************************/
    /*Funcion: UpdateBackground                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza la posicion de los backgrounds                                                                          */
    /*********************************************************************************************************************************/
    private void UpdateBackground()
    {
        difCamX = iniCamX - transform.position.x;
        bg1R.material.mainTextureOffset = new Vector2(difCamX * (scrollSpeed * 0.4f) * -1, 0.0f);
        bg2R.material.mainTextureOffset = new Vector2(difCamX * (scrollSpeed * 0.25f) * -1, 0.0f);
        bg3R.material.mainTextureOffset = new Vector2(difCamX * (scrollSpeed * 0.1f) * -1, 0.0f);
        bg4R.material.mainTextureOffset = new Vector2(difCamX * (scrollSpeed * 0.05f) * -1, 0.1f);
    }

}
