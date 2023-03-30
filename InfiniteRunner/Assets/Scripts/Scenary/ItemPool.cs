using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ItemPool : MonoBehaviour
{

    //Variables
    [SerializeField] public GameObject poolItem;
    [SerializeField] private int poolAmount;

    List<GameObject> poolObjects;


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Instancia los objetos y crea la lista (piscina de objetos) a�ade los mismo a esta lista                           */
    /*********************************************************************************************************************************/
    void Start()
    {
        poolObjects = new List<GameObject>();
        for(int i = 0; i< poolAmount; i++)
        {
            GameObject obj = Instantiate(poolItem);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }



    /*********************************************************************************************************************************/
    /*Funcion: GetPoolItem                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripci�n: Devuelve el primero objeto desactivado de la lista,si todos estan activados instancia uno nuevo y lo a�ade        */
    /*********************************************************************************************************************************/
    public GameObject GetPoolItem()
    {
        for(int i = 0;i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
                return poolObjects[i];
        }
        GameObject obj = Instantiate(poolItem);
        obj.SetActive(false);
        poolObjects.Add(obj);
        return obj;
    }
}
