using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool playerDeath;

    [SerializeField] private Transform platformGenerator;
    private Vector3 platformStartPoint;
    [SerializeField] private PlayerControl player;
    private Vector3 playerStartPoint;


    public int GetScore() { return score; }
    public bool GetPlayerDeath() { return playerDeath; }
    public void SetScore(int _score) { score += _score; }
    public void SetPlayerDeath(bool _playerDeath) { playerDeath = _playerDeath; }

    private void Awake()
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;
    }


    public void RestartGame()
    {
        playerDeath = true;
        player.gameObject.SetActive(false);
    }

    //public IEnumerator Restart()
    //{
    //    player.gameObject.SetActive(false);
    //    yield return new WaitForSeconds(0.5f);
    //    player.transform.position = platformStartPoint;
    //    platformGenerator.position = platformStartPoint;
    //    player.gameObject.SetActive(true);
    //}


}
