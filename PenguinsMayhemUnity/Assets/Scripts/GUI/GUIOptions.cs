using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIOptions : MonoBehaviour {

    private GameObject volumeMaster;
    private GameObject display;
    private GameObject windowMode;
    private GameObject closeMenu;

    // Use this for initialization
    void Start () {
        volumeMaster = transform.FindChild("VolumeMaster").gameObject;
        display = transform.FindChild("Display").gameObject;
        windowMode = transform.FindChild("WindowMode").gameObject;
        closeMenu = transform.FindChild("CloseMenu").gameObject;

        Slider volumeSlider = volumeMaster.GetComponent<Slider>();
        InitVolumeValues(ref volumeSlider);

        Dropdown displayDropdown = display.GetComponent<Dropdown>();
        InitResolutionValues(ref displayDropdown);
        SetResolutionListener(ref displayDropdown);

        Dropdown windModeDropdown = windowMode.GetComponent<Dropdown>();
        InitWinModeValues(ref windModeDropdown);
        SetWinModeListener(ref windModeDropdown);

        Button closeMenuButton = closeMenu.GetComponent<Button>();
        closeMenuListener(ref closeMenuButton);


    }


    //************************************************************************
    //VOLUME SLIDER FUNCTIONS*************************************************
    //************************************************************************
    void InitVolumeValues(ref Slider _volumeSlider)
    {
        _volumeSlider.onValueChanged.AddListener(
            delegate {
                Slider local_volumeSlider = volumeMaster.GetComponent<Slider>();
                SoundManager.SetVolume(local_volumeSlider.value);
            });
    }
    //************************************************************************
    //WINDOW RESOLUTION FUNCTIONS*********************************************
    //************************************************************************
    //Fill dropDown with values for video resolutions 
    void InitResolutionValues(ref Dropdown _displayDropdown)
    {
        _displayDropdown.options.Clear();
        foreach (WindowResolution res in VideoManager.resolutions)
        {
            string txtToAdd = res.width + "x" + res.height;
            _displayDropdown.options.Add(new Dropdown.OptionData() { text = txtToAdd });
        }
        _displayDropdown.value = 1;
        _displayDropdown.value = 0;
    }  
    //get WindowResolution from string YYYYxYYYY
    WindowResolution GetWinResFromString(string _resTxt)
    {
        int splitPos = _resTxt.IndexOf("x");
        string Wstr = _resTxt.Substring(0, splitPos);
        string Hstr = _resTxt.Substring(splitPos+1, _resTxt.Length-1 - splitPos);

        int W = int.Parse(Wstr);
        int H = int.Parse(Hstr); 
        WindowResolution winRes = new WindowResolution(W, H);
        return winRes;

    }
    //Set up listener for dropDown with resolution
    void SetResolutionListener(ref Dropdown _displayDropdown)
    {       
        _displayDropdown.onValueChanged.AddListener( 
            delegate {
                Dropdown local_displayDropdown = display.GetComponent<Dropdown>();
                WindowResolution winRes = GetWinResFromString(local_displayDropdown.captionText.text);
                VideoManager.SetResolution(winRes.width, winRes.height);
            } );
    }

    //************************************************************************
    //WINDOW MODE FUNCTIONS***************************************************
    //************************************************************************
    void InitWinModeValues(ref Dropdown winModeDropdown)
    {
        winModeDropdown.options.Clear();
        winModeDropdown.options.Add(new Dropdown.OptionData { text = "WINDOWED" });
        winModeDropdown.options.Add(new Dropdown.OptionData { text = "FULLSCREEN" });
        winModeDropdown.value = 1;
        winModeDropdown.value = 0;
    }
    void SetWinModeListener(ref Dropdown winModeDropdown)
    {
        winModeDropdown.onValueChanged.AddListener(
            delegate {
                Dropdown local_winModeDropdown = windowMode.GetComponent<Dropdown>();
                string itemText = local_winModeDropdown.captionText.text;
                VideoManager.windowMode = (itemText == "WINDOWED") ? VideoManager.WindowMode.WINDOWED : VideoManager.WindowMode.FULLSCREEN;
                VideoManager.SetResolution(VideoManager.resolution.width, VideoManager.resolution.height);            
            });
    }

    //************************************************************************
    //CLOSE MENU FUNCTIONS***************************************************
    //************************************************************************
    void closeMenuListener(ref Button _closeMenu)
    {
        _closeMenu.onClick.AddListener(delegate { Destroy(gameObject); });
    }


}
