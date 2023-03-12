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
  //  [SerializeField] private VisualTreeAsset pauseMenu;
    private VisualElement pauseButtons;
    private Button bContinue;
    private Button bExtitMenu;
    //GameOverUI
//    [SerializeField] private VisualTreeAsset gameOverMenu;
    private VisualElement gameOverButtons;
    private Button bRetry;
    private Button bExit;

    [Header("Mute Button")]
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unMuteMusicSprite;
    [SerializeField] private Sprite unMuteSoundSprite;
    private bool muteMusic;
    private bool muteSound;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        mainMenuDocument = GetComponent<UIDocument>();

        muteMusic = PlayerPrefs.GetInt("muteMusic") == 1;
        muteSound = PlayerPrefs.GetInt("muteSound") == 1;
        
        score = mainMenuDocument.rootVisualElement.Q<Label>("ScoreText");
        musicMute = mainMenuDocument.rootVisualElement.Q<Button>("BMuteMusic");
        soundMute = mainMenuDocument.rootVisualElement.Q<Button>("BMuteSound");

        pauseButtons = mainMenuDocument.rootVisualElement.Q("PauseMenu");
        bContinue = pauseButtons.Q<Button>("BContinue");
        bExtitMenu = pauseButtons.Q<Button>("BExitPause");

        gameOverButtons = mainMenuDocument.rootVisualElement.Q("GameOverPanel");
        bRetry = gameOverButtons.Q<Button>("BRetry");
        bExit = gameOverButtons.Q<Button>("BExit");

        SetCallbacks();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pause();
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
        muteMusic = !muteMusic;
        StyleBackground background = musicMute.style.backgroundImage;
        background.value = Background.FromSprite(muteMusic ? muteSprite : unMuteMusicSprite);
        musicMute.style.backgroundImage = background;
        audioMixer.SetFloat("musicVolume", muteMusic ? -80f : Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
    }

    private void MuteSound()
    {        
        muteSound = !muteSound;
        StyleBackground background = soundMute.style.backgroundImage;
        background.value = Background.FromSprite(muteSound ? muteSprite : unMuteSoundSprite);
        soundMute.style.backgroundImage = background;
        audioMixer.SetFloat("soundsVolume", muteSound ? -80f : Mathf.Log10(PlayerPrefs.GetFloat("soundsVolume")) * 20);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        pauseButtons.style.display = DisplayStyle.None;
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseButtons.style.display = DisplayStyle.Flex;        
    }


    private void GameOver()
    {
        gameOverButtons.style.display = DisplayStyle.Flex;
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
