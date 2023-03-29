using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Audio;
public class MenuController : MonoBehaviour
{
    //Variables
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
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private VisualTreeAsset settingsMenu;
    private VisualElement settingsButtons;
    private DropdownField dropDownResolution;
    private DropdownField dropDownQuality;
    private Toggle toggleFullScreen;
    private Slider sliderSound;
    private Slider sliderMusic;
    private Button bBack;
    Resolution[] resolutions;

    

    private List<string> qualityList = new List<string>(){"Low","Medium","High"};

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene las referencias de los componentes de la UI del menu principal                                            */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        
        muteMusic = PlayerPrefs.GetInt("muteMusic") == 1;
        muteSound = PlayerPrefs.GetInt("muteSound") == 1;
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

    /*********************************************************************************************************************************/
    /*Funcion: SetCallBacks                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */   
    /*Descripción: Asigna los callbacks de los botones, desplegables y slider de la UI                                               */
    /*********************************************************************************************************************************/
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

    /*********************************************************************************************************************************/
    /*Funcion: SetFullScreen                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: Booleana que activa o desactiva el fullscreen                                                           */
    /*Descripción: Activa o desactiva la pantalla completa del juego                                                                 */
    /*********************************************************************************************************************************/
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

    /*********************************************************************************************************************************/
    /*Funcion: SelectQuality                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: newQuality (calidad seleccionada)                                                                       */
    /*Descripción: Modifica la calidad de texturas del juego                                                                         */
    /*********************************************************************************************************************************/
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

    /*********************************************************************************************************************************/
    /*Funcion: PlayButtonOnClicked                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: LLama a la escena del juego                                                                                       */
    /*********************************************************************************************************************************/
    private void PlayButtonOnClicked()
    {
        SceneManager.LoadScene("Game");
    }

    /*********************************************************************************************************************************/
    /*Funcion: SettingsButtonOnClicked                                                                                               */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: LLama al panel de opciones                                                                                        */
    /*********************************************************************************************************************************/
    private void SettingsButtonOnClicked()
    {
        buttonsPanel.Clear();
        buttonsPanel.Add(settingsButtons);
    }

    /*********************************************************************************************************************************/
    /*Funcion: ExitButtonOnClicked                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Cierra el juego por completo                                                                                      */
    /*********************************************************************************************************************************/
    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetMusicVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: ev (volumen asignado)                                                                                   */
    /*Descripción: Control el Volumen de la musica del juego                                                                         */
    /*********************************************************************************************************************************/
    private void SetMusicVolume(float ev)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(ev) * 20);
        if (ev <= -0.001)
        {
            muteMusic = true;
            MuteMusicButtonOnClicked(true);
            PlayerPrefs.SetInt("muteMusic", 1);
        }
        else
        {
            muteMusic = false;
            MuteMusicButtonOnClicked(true);
            PlayerPrefs.SetInt("muteMusic", 0);
        }
        
        PlayerPrefs.SetFloat("musicVolume", sliderMusic.value);
    }

    /*********************************************************************************************************************************/
    /*Funcion: MuteMusicButtonOnClicked                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: slide (controla si se ha muteado desde el slider o desde el boton mute)                                 */
    /*Descripción: Control silenciar la musica del juego                                                                             */
    /*********************************************************************************************************************************/
    private void MuteMusicButtonOnClicked(bool slide)
    {        
        if (!slide)                  
            muteMusic = !muteMusic;         
        StyleBackground background = bMuteMusic.style.backgroundImage;
        background.value = Background.FromSprite(muteMusic ? muteSprite : unMuteMusicSprite);
        bMuteMusic.style.backgroundImage = background;        
        audioMixer.SetFloat("musicVolume", muteMusic ? -80f : Mathf.Log10(PlayerPrefs.GetFloat("musicVolume"))*20);        
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetSoundVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: ev (volumen asignado)                                                                                   */
    /*Descripción: Control el Volumen de los sonidos del juego                                                                       */
    /*********************************************************************************************************************************/
    private void SetSoundVolume(float ev)
    {
        audioMixer.SetFloat("soundsVolume", Mathf.Log10(ev) * 20);
        if (ev <= -0.001)
        {
            muteSound = true;
            MuteSoundButtonOnClicked(true);
            PlayerPrefs.SetInt("muteSound", 1);
        }
        else
        {
            muteSound = false;
            MuteSoundButtonOnClicked(true);
            PlayerPrefs.SetInt("muteSound", 0);
        }
        PlayerPrefs.SetFloat("soundsVolume", Mathf.Log10(ev) * 20);        
    }
    /*********************************************************************************************************************************/
    /*Funcion: MuteSoundButtonOnClicked                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: slide (controla si se ha muteado desde el slider o desde el boton mute)                                 */
    /*Descripción: Control silenciar los sonidos del juego                                                                           */
    /*********************************************************************************************************************************/
    private void MuteSoundButtonOnClicked(bool slide)
    {
        if (!slide)        
            muteSound = !muteSound;            
        StyleBackground background = bMuteSound.style.backgroundImage;
        background.value = Background.FromSprite(muteSound ? muteSprite : unMuteSoundSprite);
        bMuteSound.style.backgroundImage = background;
        audioMixer.SetFloat("soundsVolume", muteSound ? -80f : Mathf.Log10(PlayerPrefs.GetFloat("soundsVolume")) * 20);
    }

    /*********************************************************************************************************************************/
    /*Funcion: BackButtonOnClicked                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Carga los botones del menu principal                                                                              */
    /*********************************************************************************************************************************/
    private void BackButtonOnClicked()
    {
        buttonsPanel.Clear();
        buttonsPanel.Add(bPlay);
        buttonsPanel.Add(bSettings);
        buttonsPanel.Add(bExit);
    }
}
