using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool playerDeath;



    public int GetScore() { return score; }
    public bool GetPlayerDeath() { return playerDeath; }
    public void SetScore(int _score) { score += _score; }
    public void SetPlayerDeath(bool _playerDeath) { playerDeath = _playerDeath; }
}
