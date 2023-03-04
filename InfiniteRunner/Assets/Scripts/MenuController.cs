using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    
    private UIDocument mainMenuDocument;

    private VisualElement buttonsPanel;
    private Button bPlay;
    private Button bSettings;
    private Button bExit;
    private Button bMute;

    [Header("Mute Button")]
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unMuteSprite;
    private bool mute;


    [SerializeField] private VisualTreeAsset settingsMenu;
    private VisualElement settingsButtons;
    private DropdownField dropDownResolution;
    private DropdownField dropDownQuality;
    private Toggle toggleFullScreen;
    private Slider sliderBrightness;
    private Slider sliderAudio;
    private Button bBack;
    


    private void Awake()
    {
        mainMenuDocument = GetComponent<UIDocument>();
        buttonsPanel = mainMenuDocument.rootVisualElement.Q<VisualElement>("ButtonsMenu");
        bPlay = mainMenuDocument.rootVisualElement.Q<Button>("BPlay");
        bSettings = mainMenuDocument.rootVisualElement.Q<Button>("BSettings");
        bExit = mainMenuDocument.rootVisualElement.Q<Button>("BExit");
        bMute = mainMenuDocument.rootVisualElement.Q<Button>("BMute");
        //settings menu
        settingsButtons = settingsMenu.CloneTree();
        dropDownResolution = settingsButtons.Q<DropdownField>("DropDownResolution");
        dropDownQuality = settingsButtons.Q<DropdownField>("DropDownQuality");
        toggleFullScreen = settingsButtons.Q<Toggle>("ToggleFullScreen");
        sliderBrightness = settingsButtons.Q<Slider>("SliderBrightness");
        sliderAudio = settingsButtons.Q<Slider>("SliderAudio");
        bBack = settingsButtons.Q<Button>("BBack");
        SetCallBacks();
    } 

    private void SetCallBacks()
    {
        bPlay.clicked += PlayButtonOnClicked;
        bSettings.clicked += SettingsButtonOnClicked;
        bExit.clicked += ExitButtonOnClicked;
        bMute.clicked += MuteButtonOnClicked;
        bBack.clicked += BackButtonOnClicked;
    }


    private void PlayButtonOnClicked()
    {
        SceneManager.LoadScene("Game");
    }

    private void SettingsButtonOnClicked()
    {
        buttonsPanel.Clear();
        buttonsPanel.Add(settingsButtons);
    }

    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }

    private void MuteButtonOnClicked()
    {
        mute = !mute;
        StyleBackground background = bMute.style.backgroundImage;
        background.value = Background.FromSprite(mute ? muteSprite : unMuteSprite);
        bMute.style.backgroundImage = background;

        AudioListener.volume = mute ? 0 : 1;
    }

    private void BackButtonOnClicked()
    {
        buttonsPanel.Clear();
        buttonsPanel.Add(bPlay);
        buttonsPanel.Add(bSettings);
        buttonsPanel.Add(bExit);
    }
}
