using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;
using UnityEngine.UI;
using SaveLoadPlayerPrefs;

public class NavigationData : MonoBehaviour
{
    public static NavigationData instance;

    public delegate bool _onSetBloom(bool b);
    public static _onSetBloom OnSetBloom;

    public delegate bool _onSetFilmGrain(bool b);
    public static _onSetFilmGrain OnSetFilmGrain;

    public delegate bool _onSetChromAberration(bool b);
    public static _onSetChromAberration OnSetChromAberration;

    public delegate float _onSetVolume();
    public static _onSetVolume OnSetVolume;

    public delegate float _onSetSFX();
    public static _onSetSFX OnSetSFX;

    public delegate void _onLoading();
    public static _onLoading OnLoading;

    public delegate void _onLoadInventory();
    public static _onLoadInventory OnLoadInventory;

    public static event Action OnSaveCharacter;

    GameObject[] Datas;

    [SerializeField] int _targetFrameRate;

    [SerializeField] float _volume;
    public float Volume { get { return _volume; } }

    [SerializeField] float _sfx;
    public float Sfx { get { return _sfx; } }

    [SerializeField] bool _bloom;
    public bool Bloom { get { return _bloom; } }

    [SerializeField] bool _filmGrain;
    public bool FilmGrain { get { return _filmGrain; } }

    [SerializeField] bool _chromaticAberration;
    public bool ChromaticAberration { get { return _chromaticAberration; } }

    [SerializeField] bool _fullScreen;
    public bool FullScreen { get { return _fullScreen; } set { _fullScreen = value; } }

    [SerializeField] int _miscItems;
    public int MiscItems { get { return _miscItems; } }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Datas = GameObject.FindGameObjectsWithTag("NavigationData");

        if (Datas.Length > 1)
        {
            Destroy(Datas[1]);
        }

        Application.targetFrameRate = _targetFrameRate;
    }

    void LoadData()
    {
        SaveLoad _saveLoad = new SaveLoad();

        //FIRST TIME LOADING SETTING INITIAL VALUES!!
        if (!PlayerPrefs.HasKey(SaveStrings.FIRSTUSE.ToString()))
        {
            _saveLoad.PlayerSaveFloat(SaveStrings.VOLUME.ToString(), 1);
            _saveLoad.PlayerSaveFloat(SaveStrings.SFX.ToString(), 1);
            _saveLoad.PlayerSaveBool(SaveStrings.BLOOM.ToString(), _bloom);
            _saveLoad.PlayerSaveBool(SaveStrings.FILMGRAIN.ToString(), _filmGrain);
            _saveLoad.PlayerSaveBool(SaveStrings.CHROMATIC_ABERRATION.ToString(), _chromaticAberration);
            _saveLoad.PlayerSaveBool(SaveStrings.FULLSCREEN.ToString(), _fullScreen);
            _saveLoad.PlayerSaveInt(SaveStrings.RESOLUTION.ToString(), 0);
            _saveLoad.PlayerSaveInt(SaveStrings.MISC_ITEMS.ToString(), 0);
            _saveLoad.PlayerSaveInt(SaveStrings.MONETARY_VALUE.ToString(), 0);

            PlayerPrefs.SetInt(SaveStrings.FIRSTUSE.ToString(), 1);
        }
        //IF HAS FIRST USE KEY THE VALUES CAME FROM PLAYERPREFS KEYS
        else
        {
            _volume = PlayerPrefs.GetFloat(SaveStrings.VOLUME.ToString());

            _sfx = PlayerPrefs.GetFloat(SaveStrings.SFX.ToString());

            try
            {
                int tempCoin = _saveLoad.LoadingCoins();

                GameBehaviour gb = GameObject.FindGameObjectWithTag("GameBehaviour").GetComponent<GameBehaviour>();

                gb.PlayerMoney = tempCoin;

                gb.OnTakingCoins?.Invoke();
                    
            }
            catch
            {

            }

            _miscItems = PlayerPrefs.GetInt(SaveStrings.MISC_ITEMS.ToString());

            //BLOOM
            if (PlayerPrefs.GetString(SaveStrings.BLOOM.ToString()) == "True")
            {
                _bloom = true;
            }
            else
            {
                _bloom = false;
            }

            //FILMGRAIN
            if (PlayerPrefs.GetString(SaveStrings.FILMGRAIN.ToString()) == "True")
            {
                _filmGrain = true;
            }
            else
            {
                _filmGrain = false;
            }

            //CHROMATIC ABERRATION
            if (PlayerPrefs.GetString(SaveStrings.CHROMATIC_ABERRATION.ToString()) == "True")
            {
                _chromaticAberration = true;
            }
            else
            {
                _chromaticAberration = false;
            }

            //FULLSCREEN
            if (PlayerPrefs.GetString(SaveStrings.FULLSCREEN.ToString()) == "True")
            {
                _fullScreen = true;
            }
            else
            {
                _fullScreen = false;
            }

            //RESOLUTION
            switch (PlayerPrefs.GetInt(SaveStrings.RESOLUTION.ToString()))
            {
                case 0:
                    {
                        Screen.SetResolution(1280, 720, FullScreen);
                        break;
                    }
                case 1:
                    {
                        Screen.SetResolution(1920, 1080, FullScreen);
                        break;
                    }
                case 2:
                    {
                        Screen.SetResolution(2560, 1440, FullScreen);
                        break;
                    }
                case 3:
                    {
                        Screen.SetResolution(3840, 2160, FullScreen);
                        break;
                    }
            }
        }

        OnSetBloom?.Invoke(_bloom);
        OnSetChromAberration?.Invoke(_chromaticAberration);
        OnSetFilmGrain?.Invoke(_filmGrain);
    }
    
    public bool SetBloom(bool b)
    {
        Volume vol = GameObject.FindGameObjectWithTag("Global Volume").GetComponent<Volume>();

        Bloom bloomVolume;

        if(vol.profile.TryGet<Bloom>(out bloomVolume))
        {
            if(b)
            {
                bloomVolume.scatter.value = .6f;
            }
            else
            {
                bloomVolume.scatter.value = .3f;
            }
        }

        try
        {
            Toggle t = GameObject.FindGameObjectWithTag("BloomFX").GetComponent<Toggle>();

            if (t != null) t.isOn = b;
        }
        catch
        {

        }

        return _bloom = b;
    }

    public bool SetFilmGrain(bool b)
    {
        Volume vol = GameObject.FindGameObjectWithTag("Global Volume").GetComponent<Volume>();

        FilmGrain filmGrainVolume;

        if (vol.profile.TryGet<FilmGrain>(out filmGrainVolume))
        {
            filmGrainVolume.active = b;
        }

        try
        {
            Toggle t = GameObject.FindGameObjectWithTag("FilmGrainFX").GetComponent<Toggle>();

            if (t != null) t.isOn = b;
        }
        catch
        {

        }

        return _filmGrain = b;
    }

    public bool SetChromaticAberration(bool b)
    {
        Volume vol = GameObject.FindGameObjectWithTag("Global Volume").GetComponent<Volume>();

        ChromaticAberration chromaticAberrationVolume;

        if (vol.profile.TryGet<ChromaticAberration>(out chromaticAberrationVolume))
        {
            chromaticAberrationVolume.active = b;
        }
        
        try
        {
            Toggle t = GameObject.FindGameObjectWithTag("ChromaFX").GetComponent<Toggle>();

            if (t != null) t.isOn = b;
        }
        catch
        {

        }

        return _chromaticAberration = b;
    }

    public float SetVolumeValue(float f)
    {
        return _volume = f;
    }
    public float SetSfxValue(float f)
    {
        return _sfx = f;
    }

    public float SetSoundVolume()
    {
        try
        {
            GameObject[] audio = GameObject.FindGameObjectsWithTag("VolumeSound");

            foreach (GameObject audioObj in audio)
            {
                audioObj.GetComponent<AudioSource>().volume = _volume;
            }
        }
        catch
        {
            Instantiate(Resources.Load("SoundSource"));

            AudioSource audio = GameObject.FindGameObjectWithTag("VolumeSound").GetComponent<AudioSource>();

            if (audio != null) audio.volume = _volume;
        }

        return _volume;
    }

    public float SetSfxVolume()
    {
        try
        {
            GameObject[] audio = GameObject.FindGameObjectsWithTag("VolumeSFX");

            foreach (GameObject audioSfx in audio)
            {
                audioSfx.GetComponent<AudioSource>().volume = _sfx;
            }
        }
        catch
        {
            Instantiate(Resources.Load("SFXSource"));

            AudioSource audio = GameObject.FindGameObjectWithTag("VolumeSFX").GetComponent<AudioSource>();

            if (audio != null) audio.volume = _sfx;
        }

        return _sfx;
    }

    public int TakingMiscItems()
    {
        return _miscItems++;
    }

    public int CleanMiscItems()
    {
        return _miscItems = 0;
    }

    private void OnEnable()
    {
        OnLoading += LoadData;
        OnSetBloom += SetBloom;
        OnSetVolume += SetSoundVolume;
        OnSetSFX += SetSfxVolume;
        OnSetFilmGrain += SetFilmGrain;
        OnSetChromAberration += SetChromaticAberration;
    }

    private void OnDisable()
    {
        OnLoading -= LoadData;
        OnSetBloom -= SetBloom;
        OnSetVolume -= SetSoundVolume;
        OnSetSFX -= SetSfxVolume;
        OnSetFilmGrain -= SetFilmGrain;
        OnSetChromAberration -= SetChromaticAberration;
    }
}

