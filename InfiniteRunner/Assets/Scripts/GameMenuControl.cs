using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Audio;
public class GameMenuControl : MonoBehaviour
{
    private UIDocument mainMenuDocument;
    //GameUI
    private Label score;
    private Button soundMute;
    private Button musicMute;
    //PauseUI
    private Button bContinue;
    private Button bExtitMenu;
    //GameOverUI
    private Button bRetry;
    private Button bExit;

    private bool muteMusic;
    private bool muteSound;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {

        muteMusic = PlayerPrefs.GetInt("muteMusic") == 1;
        muteSound = PlayerPrefs.GetInt("muteSound") == 1;
        mainMenuDocument = GetComponent<UIDocument>();
        score = mainMenuDocument.rootVisualElement.Q<Label>("ScoreText");
        musicMute = mainMenuDocument.rootVisualElement.Q<Button>("BMuteMusic");
        soundMute = mainMenuDocument.rootVisualElement.Q<Button>("BMuteSound");

        bContinue = mainMenuDocument.rootVisualElement.Q<Button>("BContinue");
        bExtitMenu = mainMenuDocument.rootVisualElement.Q<Button>("BExitPause");
        bRetry = mainMenuDocument.rootVisualElement.Q<Button>("BRetry");
        bExit = mainMenuDocument.rootVisualElement.Q<Button>("BExit");

        SetCallbacks();
    }

    private void SetCallbacks()
    {
        musicMute.clicked += MuteMusic;
        soundMute.clicked += MuteSound;
        bContinue.clicked += ContinueGame;
        bExtitMenu.clicked += Exit;
        bRetry.clicked += Retry;
        bExit.clicked += Exit;
    }

    private void MuteMusic()
    {
       
    }

    private void MuteSound()
    {

    }

    private void ContinueGame()
    {

    }

    private void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Retry()
    {
        SceneManager.LoadScene("Game");
    }
}
