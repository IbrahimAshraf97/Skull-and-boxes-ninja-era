using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundSettingManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackGroundPref = "BackGroundPref";

    private int _firstPlayInt;
    public Slider _bkGroundSlider;
    private float _bkGroundSliderValue;
    public AudioSource _bkGroundAudioSource;

    public static SoundSettingManager Instance;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if (_firstPlayInt == 0)
        {
            _bkGroundSliderValue = 0.25f;
            _bkGroundSlider.value = _bkGroundSliderValue;
            PlayerPrefs.SetFloat(BackGroundPref, _bkGroundSliderValue);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            _bkGroundSliderValue = PlayerPrefs.GetFloat(BackGroundPref);
            _bkGroundSlider.value = _bkGroundSliderValue;

        }
    }
    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(BackGroundPref, _bkGroundSlider.value);

    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSetting();
        }
    }
    public void UpdateSound()
    {
        _bkGroundAudioSource.volume = _bkGroundSlider.value;
    }
}
