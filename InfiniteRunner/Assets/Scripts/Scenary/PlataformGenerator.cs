using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformGenerator : MonoBehaviour
{
    //Variables
    [SerializeField] private Transform generationPoint;
    [SerializeField] private float distance;
    private float minDistance = 4;
    private float maxDistance = 10;
    private int platformSelected;
    private float[] platformsWidth;
    [SerializeField] private ItemPool[] poolPlatforms;
    
    private float minHeight;
    private float maxHeight;
    [SerializeField] private Transform maxHeightPoint;
    [SerializeField] private float maxHeightChange;
    private float heightChange;

    private CoinGenerator coinGenerator;
    [SerializeField] private float randomCoin;

    [SerializeField] private float randomSpike;
    [SerializeField] ItemPool SpikePool;

    [SerializeField] private float powerUpHeight;
    [SerializeField] private ItemPool [] powerUpPool;
    [SerializeField] private float randomPowerUp;

    //Getters & Setters
    public float GetRandomSpike() { return randomSpike; }
    public float GetMinDistance() { return minDistance; }
    public float GetMaxDistance() { return maxDistance; }
    public float GetMaxHeightChange() { return maxHeightChange; }
    public void SetRandomSpike(float _randomSpike) { randomSpike = _randomSpike; }
    public void SetMinDistance(float _minDistance) { minDistance = _minDistance; }
    public void SetMaxDistance(float _maxDistance) { maxDistance = _maxDistance; }
    public void SetMaxHeightChange(float _maxHeightChange) { maxHeightChange = _maxHeightChange;  }


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene los anchos de las plataformas y genera lo valor minimo y maximo de altura de generacion                   */
    /*********************************************************************************************************************************/
    void Start()
    {     
        platformsWidth = new float[poolPlatforms.Length];
        for (int i = 0; i < poolPlatforms.Length; i++)
            platformsWidth[i] = poolPlatforms[i].poolItem.GetComponent<BoxCollider2D>().size.x;

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        coinGenerator = FindObjectOfType<CoinGenerator>();
    }

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera los componentes de forma aleatoria (plataforma,powerups,monedas,pinchos)                                   */
    /*********************************************************************************************************************************/
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distance = Random.Range(minDistance, maxDistance);
            platformSelected = Random.Range(0, poolPlatforms.Length);
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if(heightChange > maxHeight)            
                heightChange = maxHeight;
            else if(heightChange < minHeight)
                heightChange = minHeight;

            GeneratePowerUp();
            GeneratePlatform();
            GenerateCoin();
            GenerateSpike();
            transform.position = new Vector3(transform.position.x + (platformsWidth[platformSelected] / 2), transform.position.y, transform.position.z);
        }
    }


    /*********************************************************************************************************************************/
    /*Funcion: GeneratePlatform                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera una de las plataformas de forma aleatoria                                                                  */
    /*********************************************************************************************************************************/
    private void GeneratePlatform()
    {
        transform.position = new Vector3(transform.position.x + (platformsWidth[platformSelected] / 2) + distance, heightChange, transform.position.z);

        Instantiate(poolPlatforms[platformSelected], transform.position, transform.rotation);
        GameObject newPlatform = poolPlatforms[platformSelected].GetPoolItem();

        newPlatform.transform.position = transform.position;
        newPlatform.transform.rotation = transform.rotation;
        newPlatform.SetActive(true);
    }


    /*********************************************************************************************************************************/
    /*Funcion: GeneratePowerUp                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera uno de los powerups de forma aleatoria                                                                     */
    /*********************************************************************************************************************************/
    private void GeneratePowerUp()
    {
        if (Random.Range(0f, 100) < randomPowerUp)
        {
            GameObject newPowerUp = powerUpPool[Random.Range(0, powerUpPool.Length)].GetPoolItem();
            newPowerUp.transform.position = transform.position + new Vector3(distance / 2f, powerUpHeight, 0f);
            newPowerUp.SetActive(true);
        }
    }


    /*********************************************************************************************************************************/
    /*Funcion: GenerateCoin                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera monedas de forma aleatoria                                                                                 */
    /*********************************************************************************************************************************/
    private void GenerateCoin()
    {
        if (Random.Range(0f, 100f) < randomCoin)
            coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z));
    }



    /*********************************************************************************************************************************/
    /*Funcion: GenerateSpike                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera pinchos de forma aleatoria                                                                                 */
    /*********************************************************************************************************************************/
    private void GenerateSpike()
    {
        if (Random.Range(0f, 100f) < randomSpike)
        {
            GameObject spike = SpikePool.GetPoolItem();

            Vector3 spikePos = new Vector3(Random.Range(-platformsWidth[platformSelected] / 2 + 1f, platformsWidth[platformSelected] / 2 - 1f), 2f, 0f);
            spike.transform.position = transform.position + spikePos;
            spike.transform.rotation = transform.rotation;
            spike.SetActive(true);
        }


    }
}
