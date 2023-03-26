using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float score = 0;
    private float highScore;
    private bool playerDeath;
    private float pointForSecond = 1;

    [SerializeField] private Transform platformGenerator;    
    [SerializeField] private PlayerControl player;

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource backgroundDeathMusic;

    public float GetScore() { return score; }
    public float GetHighScore() { return highScore; }
    public bool GetPlayerDeath() { return playerDeath; }
    public float GetPointForSecond() { return pointForSecond; }
    public void SetScore(float _score) { score += _score*pointForSecond; }
    public void SetHighScore(float _highScore) { score += _highScore; }
    public void SetPlayerDeath(bool _playerDeath) { playerDeath = _playerDeath; }
    public void SetPointForSecond(float _pointforSecond) { pointForSecond = _pointforSecond; }

    private void Awake()
    {
        highScore = PlayerPrefs.GetFloat("HighScore",0);
    }
    private void Update()
    {
        score += pointForSecond * Time.deltaTime;
        if (score > highScore)
            highScore = score;

    }

    public void RestartGame()
    {        
        playerDeath = true;
        player.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("HighScore", highScore);
        backgroundMusic.Stop();
        backgroundDeathMusic.Play();
    }
}
