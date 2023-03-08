using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Audio;
public class MenuController : MonoBehaviour
{
    
    private UIDocument mainMenuDocument;

    private VisualElement buttonsPanel;
    private Button bPlay;
    private Button bSettings;
    private Button bExit;
    private Button bMuteMusic;
    private Button bMuteSound;

    [Header("Mute Button")]
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unMuteMusicSprite;
    [SerializeField] private Sprite unMuteSoundSprite;
    private bool muteMusic;
    private bool muteSound;


    [SerializeField] private VisualTreeAsset settingsMenu;
    private VisualElement settingsButtons;
    private DropdownField dropDownResolution;
    private DropdownField dropDownQuality;
    private Toggle toggleFullScreen;
    private Slider sliderSound;
    private Slider sliderMusic;
    private Button bBack;
    Resolution[] resolutions;

    [SerializeField] private AudioMixer audioMixer;

    
    

    private List<string> qualityList = new List<string>()
    {
        "Low",
        "Medium",
        "High"
    };

    private void Awake()
    {
        
        mainMenuDocument = GetComponent<UIDocument>();
        buttonsPanel = mainMenuDocument.rootVisualElement.Q<VisualElement>("ButtonsMenu");
        bPlay = mainMenuDocument.rootVisualElement.Q<Button>("BPlay");
        bSettings = mainMenuDocument.rootVisualElement.Q<Button>("BSettings");
        bExit = mainMenuDocument.rootVisualElement.Q<Button>("BExit");
        bMuteMusic = mainMenuDocument.rootVisualElement.Q<Button>("BMuteMusic");
        bMuteSound = mainMenuDocument.rootVisualElement.Q<Button>("BMuteSound");
        //settings menu
        settingsButtons = settingsMenu.CloneTree();
        dropDownResolution = settingsButtons.Q<DropdownField>("DropDownResolution");
        TakeAllResolutions();        
        dropDownQuality = settingsButtons.Q<DropdownField>("DropDownQuality");
        dropDownQuality.choices = qualityList;
        dropDownQuality.index = 0;
        toggleFullScreen = settingsButtons.Q<Toggle>("ToggleFullScreen");
        sliderSound = settingsButtons.Q<Slider>("SliderSound");
        sliderMusic = settingsButtons.Q<Slider>("SliderMusic");
        bBack = settingsButtons.Q<Button>("BBack");
        SetCallBacks();
    }

    private void SetCallBacks()
    {
        bPlay.clicked += PlayButtonOnClicked;
        bSettings.clicked += SettingsButtonOnClicked;
        bExit.clicked += ExitButtonOnClicked;
        bMuteMusic.RegisterCallback<ClickEvent>(ev => MuteMusicButtonOnClicked(false));
        bMuteSound.RegisterCallback<ClickEvent>(ev => MuteSoundButtonOnClicked(false));

        dropDownResolution.RegisterValueChangedCallback(value => SetResolution(dropDownResolution.index));
        dropDownResolution.index = 0;
        dropDownQuality.RegisterValueChangedCallback(value => SelectQuality(dropDownQuality.value));
        toggleFullScreen.RegisterCallback<MouseUpEvent>(ev => { SetFullScreen(toggleFullScreen.value); }, TrickleDown.TrickleDown);
        
        sliderSound.RegisterValueChangedCallback(ev => SetSoundVolume(sliderSound.value));
        sliderMusic.RegisterValueChangedCallback(ev => SetMusicVolume(sliderMusic.value));
        bBack.clicked += BackButtonOnClicked;
    }

    private void SetFullScreen(bool check)
    {
        Screen.fullScreen = check;
    }

    /*********************************************************************************************************************************/
    /*Funcion: TakeAllResolutions                                                                                                    */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene las resoluciones posibles de la pantalla para generarlas en el dropbox de resoluciones sin que se repitan */
    /*********************************************************************************************************************************/
    private void TakeAllResolutions()
    {
        resolutions = Screen.resolutions;               
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (i >= 2 && (resolutions[i].width != resolutions[i - 1].width || resolutions[i].height != resolutions[i - 1].height))
                options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        dropDownResolution.choices = options;
        dropDownResolution.index = currentResolutionIndex;
        
    }
    /*********************************************************************************************************************************/
    /*Funcion: SetResolution                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: resolutionIndex (resolucion seleccionada del dropbox)                                                   */
    /*Descripción: Modifica la resolucion del juego                                                                                  */
    /*********************************************************************************************************************************/
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);       
    }
    
    private void SelectQuality(string newQuality)
    {
        int quality = 0;
        if (newQuality == "Low")
            quality = 0;
        else if (newQuality == "Medium")
            quality = 1;
        else if (newQuality == "High")
            quality = 2;
        QualitySettings.SetQualityLevel(quality);
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

    private void SetMusicVolume(float ev)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(ev) * 20);
        if (ev <= -0.001)
            muteMusic = true;
        else
            muteMusic = false;
        PlayerPrefs.SetFloat("musicVolume", Mathf.Log10(ev) * 20);
        MuteMusicButtonOnClicked(true);
        
    }
    private void MuteMusicButtonOnClicked(bool slide)
    {
        if(!slide)
            muteMusic = !muteMusic;
        StyleBackground background = bMuteMusic.style.backgroundImage;
        background.value = Background.FromSprite(muteMusic ? muteSprite : unMuteMusicSprite);
        bMuteMusic.style.backgroundImage = background;
        audioMixer.SetFloat("musicVolume", muteMusic ? -80f : PlayerPrefs.GetFloat("musicVolume"));
        
    }
    private void SetSoundVolume(float ev)
    {

        audioMixer.SetFloat("soundsVolume", Mathf.Log10(ev) * 20);
        if (ev <= -0.001)
            muteSound = true;
        else
            muteSound = false;
        PlayerPrefs.SetFloat("soundsVolume", Mathf.Log10(ev) * 20);
        MuteSoundButtonOnClicked(true);
    }
    private void MuteSoundButtonOnClicked(bool slide)
    {
        if (!slide)        
            muteSound = !muteSound;            
        StyleBackground background = bMuteSound.style.backgroundImage;
        background.value = Background.FromSprite(muteSound ? muteSprite : unMuteSoundSprite);
        bMuteSound.style.backgroundImage = background;
        audioMixer.SetFloat("soundsVolume", muteSound ? -80f : PlayerPrefs.GetFloat("soundsVolume"));
        
    }
    private void BackButtonOnClicked()
    {
        buttonsPanel.Clear();
        buttonsPanel.Add(bPlay);
        buttonsPanel.Add(bSettings);
        buttonsPanel.Add(bExit);
    }
}
