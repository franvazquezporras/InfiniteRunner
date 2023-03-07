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
        bMute = mainMenuDocument.rootVisualElement.Q<Button>("BMute");
        //settings menu
        settingsButtons = settingsMenu.CloneTree();
        dropDownResolution = settingsButtons.Q<DropdownField>("DropDownResolution");
        TakeAllResolutions();        
        dropDownQuality = settingsButtons.Q<DropdownField>("DropDownQuality");
        dropDownQuality.choices = qualityList;
        dropDownQuality.index = 0;
        toggleFullScreen = settingsButtons.Q<Toggle>("ToggleFullScreen");
        sliderBrightness = settingsButtons.Q<Slider>("SliderBrightness");
        sliderAudio = settingsButtons.Q<Slider>("SliderVolume");
        bBack = settingsButtons.Q<Button>("BBack");
        SetCallBacks();
    }

    private void SetCallBacks()
    {
        bPlay.clicked += PlayButtonOnClicked;
        bSettings.clicked += SettingsButtonOnClicked;
        bExit.clicked += ExitButtonOnClicked;
        bMute.clicked += MuteButtonOnClicked;

        dropDownResolution.RegisterValueChangedCallback(value => SetResolution(dropDownResolution.index));
        dropDownResolution.index = 0;
        dropDownQuality.RegisterValueChangedCallback(value => SelectQuality(dropDownQuality.value));
        toggleFullScreen.RegisterCallback<MouseUpEvent>(ev => { SetFullScreen(toggleFullScreen.value); }, TrickleDown.TrickleDown);
        sliderBrightness.RegisterValueChangedCallback(SetBrightness);
        //sliderAudio.RegisterValueChangedCallback(SetMasterVolume);
        sliderAudio.RegisterValueChangedCallback(ev => SetMasterVolume(sliderAudio.value));
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

    private void SetMasterVolume(float ev)
    {
        if(ev==0)
            audioMixer.SetFloat("masterVolume", -80);
        else
            audioMixer.SetFloat("masterVolume", Mathf.Log10(ev) * 20);
    }
    private void SetBrightness(ChangeEvent<float> ev)
    {        
        Screen.brightness = ev.newValue;
        Debug.Log(Screen.brightness+"      "+ev.newValue);
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
