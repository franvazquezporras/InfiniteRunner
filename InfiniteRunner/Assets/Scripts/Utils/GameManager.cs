using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    private float score = 0;
    private float highScore;
    private bool playerDeath;
    private float pointForSecond = 1;

    [SerializeField] private Transform platformGenerator;    
    [SerializeField] private PlayerControl player;

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource backgroundDeathMusic;

    //Getters & Setters
    public float GetScore() { return score; }
    public float GetHighScore() { return highScore; }
    public bool GetPlayerDeath() { return playerDeath; }
    public float GetPointForSecond() { return pointForSecond; }
    public void SetScore(float _score) { score += _score*pointForSecond; }
    public void SetHighScore(float _highScore) { score += _highScore; }
    public void SetPlayerDeath(bool _playerDeath) { playerDeath = _playerDeath; }
    public void SetPointForSecond(float _pointforSecond) { pointForSecond = _pointforSecond; }

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene el ultimo highscore guardado en playerpref                                                                */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        highScore = PlayerPrefs.GetFloat("HighScore",0);
    }

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza los puntos en base al tiempo, en caso de superar el record este tambien se actualiza                    */
    /*********************************************************************************************************************************/
    private void Update()
    {
        score += pointForSecond * Time.deltaTime;
        if (score > highScore)
            highScore = score;

    }


    /*********************************************************************************************************************************/
    /*Funcion: RestartGame                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Activa el menu de game over y guarda el record, activa la musica de game over                                     */
    /*********************************************************************************************************************************/
    public void RestartGame()
    {        
        playerDeath = true;
        player.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("HighScore", highScore);
        backgroundMusic.Stop();
        backgroundDeathMusic.Play();
    }
}
