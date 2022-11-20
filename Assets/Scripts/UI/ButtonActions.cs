using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadPlayerPrefs;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    [SerializeField] Slider _sound, _sfx;

    [SerializeField] TMPro.TMP_Dropdown _resolution;

    [SerializeField] TMPro.TMP_Dropdown _qualitySettings;

    [SerializeField] Toggle _fullScreen;

    public void OnEnterOptions()
    {
        GetComponent<Animator>().SetTrigger("OPTIONS");
    }

    public void OnEnterCredits()
    {
        GetComponent<Animator>().SetTrigger("CREDITS");
    }

    public void OnMainMenu()
    {
        GetComponent<Animator>().SetTrigger("MAINMENU");
    }

    public void OnClearData()
    {
        GetComponent<Animator>().SetTrigger("CLEARSAVE");
    }

    public void SetQuality(int qualitySettings)
    {
        QualitySettings.SetQualityLevel(qualitySettings);
    }

    public void SetResolution(int res)
    {
        SaveLoad s = new SaveLoad();

        switch (res)
        {
            case 0:
                {
                    Screen.SetResolution(1280, 720, NavigationData.instance.FullScreen);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1920, 1080, NavigationData.instance.FullScreen);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(2560, 1440, NavigationData.instance.FullScreen);
                    break;
                }
            case 3:
                {
                    Screen.SetResolution(3840, 2160, NavigationData.instance.FullScreen);
                    break;
                }
        }

        s.PlayerSaveInt(SaveStrings.RESOLUTION.ToString(), res);
    }

    public void SetFullScreen(bool on)
    {
        SaveLoad s = new SaveLoad();

        Screen.fullScreen = on;

        NavigationData.instance.FullScreen = on;

        s.PlayerSaveBool(SaveStrings.FULLSCREEN.ToString(), on);
    }

    public void SaveBloom(bool v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveBool(SaveStrings.BLOOM.ToString(), v);

        NavigationData.OnSetBloom?.Invoke(v);
    }

    public void SaveFilmGrain(bool v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveBool(SaveStrings.FILMGRAIN.ToString(), v);

        NavigationData.OnSetFilmGrain?.Invoke(v);
    }

    public void SaveChromaticAberration(bool v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveBool(SaveStrings.CHROMATIC_ABERRATION.ToString(), v);

        NavigationData.OnSetChromAberration?.Invoke(v);
    }

    public void SaveVolume(float v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveFloat(SaveStrings.VOLUME.ToString(), v);

        NavigationData.instance.SetVolumeValue(v);

        NavigationData.OnSetVolume?.Invoke();
    }
    public void SaveSFX(float v)
    {
        SaveLoad s = new SaveLoad();

        s.PlayerSaveFloat(SaveStrings.SFX.ToString(), v);

        NavigationData.instance.SetSfxValue(v);

        NavigationData.OnSetSFX?.Invoke();
    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void UpdateUIOnLoad()
    {
        _sound.value = PlayerPrefs.GetFloat(SaveStrings.VOLUME.ToString());

        _sfx.value = PlayerPrefs.GetFloat(SaveStrings.SFX.ToString());

        Toggle tChroma = GameObject.FindGameObjectWithTag("ChromaFX").GetComponent<Toggle>();

        if (PlayerPrefs.GetString(SaveStrings.CHROMATIC_ABERRATION.ToString()) == "True")
        {
            tChroma.isOn = true;
        }
        else
        {
            tChroma.isOn = false;
        }

        Toggle tBlomm = GameObject.FindGameObjectWithTag("BloomFX").GetComponent<Toggle>();

        if (PlayerPrefs.GetString(SaveStrings.BLOOM.ToString()) == "True")
        {
            tBlomm.isOn = true;
        }
        else
        {
            tBlomm.isOn = false;
        }

        Toggle tGrain = GameObject.FindGameObjectWithTag("FilmGrainFX").GetComponent<Toggle>();

        if (PlayerPrefs.GetString(SaveStrings.FILMGRAIN.ToString()) == "True")
        {
            tGrain.isOn = true;
        }
        else
        {
            tGrain.isOn = false;
        }

        if (PlayerPrefs.GetString(SaveStrings.FULLSCREEN.ToString()) == "True")
        {
            _fullScreen.isOn = true;
        }
        else
        {
            _fullScreen.isOn = false;
        }

        _resolution.value = PlayerPrefs.GetInt(SaveStrings.RESOLUTION.ToString());

        switch (_resolution.value)
        {
            case 0:
                {
                    Screen.SetResolution(1280, 720, _fullScreen.isOn);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1920, 1080, _fullScreen.isOn);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(2560, 1440, _fullScreen.isOn);
                    break;
                }
            case 3:
                {
                    Screen.SetResolution(3840, 2160, _fullScreen.isOn);
                    break;
                }
        }
    }

    private void OnEnable()
    {
        NavigationData.OnLoading += UpdateUIOnLoad;
    }
    private void OnDisable()
    {
        NavigationData.OnLoading -= UpdateUIOnLoad;
    }
}
